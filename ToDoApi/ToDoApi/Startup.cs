using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Security.Claims;
using ToDoApi.Security;
using ToDoApi.Services;
using ToDoInfrastructure;

namespace ToDoApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ToDoDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ToDoDbContext")));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });


            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "ToDo List API",
                        Description = "API for ToDo List",
                        Version = "v1"
                    });
            });

            services.AddScoped<ToDoListsService>();

            string domain = "https://dev-j35kt5s7.us.auth0.com/";
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = domain;
                    options.Audience = "https://todos";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("create:todolist", policy => policy.Requirements.Add(new HasScopeRequirement("create:todolist", domain)));
                options.AddPolicy("read:todolist", policy => policy.Requirements.Add(new HasScopeRequirement("read:todolist", domain)));
                options.AddPolicy("update:todolist", policy => policy.Requirements.Add(new HasScopeRequirement("update:todolist", domain)));
                options.AddPolicy("delete:todolist", policy => policy.Requirements.Add(new HasScopeRequirement("delete:todolist", domain)));

                options.AddPolicy("create:todoitem", policy => policy.Requirements.Add(new HasScopeRequirement("create:todoitem", domain)));
                options.AddPolicy("read:todoitem", policy => policy.Requirements.Add(new HasScopeRequirement("read:todoitem", domain)));
                options.AddPolicy("update:todoitem", policy => policy.Requirements.Add(new HasScopeRequirement("update:todoitem", domain)));
                options.AddPolicy("delete:todoitem", policy => policy.Requirements.Add(new HasScopeRequirement("delete:todoitem", domain)));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ToDoDbContext>();
                context.Database.Migrate();
            }
            catch(Exception)
            {

            }

            app.UseCors("AllowAll");

            app.UseMvc();

            app.UseSwagger();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("v1/swagger.json", "ToDo list API");
            });
            
        }
    }
}
