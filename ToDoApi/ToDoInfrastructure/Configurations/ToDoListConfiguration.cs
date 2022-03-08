using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoCore;

namespace ToDoInfrastructure
{
    public class ToDoListConfiguration : IEntityTypeConfiguration<ToDoList>
    {
        public void Configure(EntityTypeBuilder<ToDoList> builder)
        {
            builder.HasMany(list => list.ListItems)
                .WithOne(item => item.ToDoList)
                .HasForeignKey(item => item.ToDoListId);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
