using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Entities;
using ToDo.Identity.Models;

namespace ToDo.Identity.DbContext
{
    public class ToDoIdentityContext : IdentityDbContext<ApplicationUser>
    {
        public ToDoIdentityContext(DbContextOptions<ToDoIdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ToDoIdentityContext).Assembly);
        }
    }
}
