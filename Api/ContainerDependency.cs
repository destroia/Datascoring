using Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public static class ContainerDependency
    {
        public static IServiceCollection AddInjApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddData(configuration);

            return services;
        }
    }
}
