using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Application.Contracts.Data;
using ToDo.Data.DatabaseContext;
using ToDo.Data.Repositories;
using ToDo.Domain.Entities;

namespace ToDo.Data
{
    public static class DataServiceRegistration
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ToDoContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AkasiaToDoConnectionString"));
            });

            // Register a generic repository for CRUD operations, using a scoped lifetime.
            // Scoped lifetime means a new instance is created for each request.
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IToDoActivityRepository, ToDoActivityRepository>();

            // Ensure the database is created or migrated when the application starts.
            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ToDoContext>();

                //  Uncomment one of the following lines depending on your requirements:
                //dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated(); // Creates the database if it does not exist.
                //dbContext.Database.Migrate(); // Applies any pending migrations.
            }

            return services;
        }
    }
}
