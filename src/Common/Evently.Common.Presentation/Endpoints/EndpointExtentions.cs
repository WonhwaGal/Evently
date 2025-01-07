using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Common.Presentation.Endpoints;
public static class EndpointExtentions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services, params Assembly[] assemblies)
    {
        // Регистрация AutoMapper для сервисов презентационного уровня
        services.AddAutoMapper(assemblies);

        return services;
    }
}
