using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Application.Data;
using Evently.Common.Infrastructure.Data;
using Evently.Common.Infrastructure.Interceptors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Evently.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        services.AddSingleton<IDbConnectionFactory>(_ => 
            new SqlConnectionFactory(databaseConnectionString));

        services.TryAddSingleton<PublishDomainEventsInterceptor>();

        return services;
    }
}
