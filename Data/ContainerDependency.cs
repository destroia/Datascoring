using Data.Data;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class ContainerDependency
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatascoringDBContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("ConnectionMain")));

            services.AddScoped<IUser, UserData>();

            return services;
        }
    }
}
