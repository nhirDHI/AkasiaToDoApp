using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Entities;

namespace ToDo.Data.ModelConfigurations
{
    public class ToDoActivityConfiguration : IEntityTypeConfiguration<ToDoActivity>
    {
        public void Configure(EntityTypeBuilder<ToDoActivity> builder)
        {
            // Table and primary key configuration
            builder.ToTable("ToDoActivity", schema: "AkasiaToDo");
            builder.HasKey(p => p.Id);
        }
    }
}
