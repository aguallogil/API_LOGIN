using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DALServicesRegistration
    {
        public static IServiceCollection AddDALServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MiDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
