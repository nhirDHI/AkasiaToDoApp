using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Contracts.Identity;
using ToDo.Data.ModelConfigurations;
using ToDo.Domain.Common;
using ToDo.Domain.Entities;

namespace ToDo.Data.DatabaseContext
{
    public class ToDoContext : DbContext
    {
        private readonly IUserService _userService;

        public ToDoContext(DbContextOptions<ToDoContext> options, IUserService userService) : base(options)
        {
            this._userService = userService;
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
                entry.Entity.UpdatedUser = _userService.UserId;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.CreatedUser = _userService.UserId;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
