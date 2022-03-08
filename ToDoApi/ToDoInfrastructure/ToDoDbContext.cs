using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToDoCore;


namespace ToDoInfrastructure
{
    public class ToDoDbContext : DbContext
    {
       

        public ToDoDbContext()
        {
        }

        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) 
            : base(options)
        {
           
        }

        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<ListItem> ListItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
   
    }
}
