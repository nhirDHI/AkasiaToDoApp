using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Data.ModelConfigurations;
using ToDo.Domain.Common;
using ToDo.Domain.Entities;

namespace ToDo.Data.DatabaseContext
{
    public class ToDoContext : IdentityDbContext<ApplicationUser>
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
        }

        public DbSet<ToDoActivity> ToDoActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Identity-specific configurations
            base.OnModelCreating(modelBuilder);

            // Apply configurations from ToDoContext
            modelBuilder.ApplyConfiguration(new ToDoActivityConfiguration());

            // Apply configurations for Identity (optional)
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoContext).Assembly);

        }

        // SaveChangesAsync logic from ToDoContext
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.UpdatedDate = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
