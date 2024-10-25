using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Data.DatabaseContext;

namespace ToDo.Data
{
    public static class DataServiceRegistration
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ToDoContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("AkasiaToDoConnectionString"));
            });

            return services;
        }
    }
}
