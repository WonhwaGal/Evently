using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Evently.Common.Presentation.Endpoints;
public static class EndpointExtentions
{
    /// <summary>
    /// Добавить конечные точки
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public static IServiceCollection AddEndpoints(this IServiceCollection services, params Assembly[] assemblies)
    {
        ServiceDescriptor[] serviceDescriptors = assemblies
            .SelectMany(x => x.GetTypes())
            // Фильтрация типов, которые не являются абстрактными классами или интерфейсами
            .Where(type => type is { IsAbstract: false, IsInterface: false } && type.IsAssignableTo(typeof(IEndpoint)))
            // Преобразование типов в сервисы
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        // Добавление сервисов в коллекцию
        services.TryAddEnumerable(serviceDescriptors);

        // Регистрация AutoMapper для сервисов презентационного уровня
        services.AddAutoMapper(assemblies);

        return services;
    }

    
    /// <summary>
    /// Маппинг конечных точек
    /// </summary>
    /// <param name="app"></param>
    /// <param name="routeGroupBuilder"></param>
    /// <returns></returns>
    public static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();
        IEndpointRouteBuilder builder = routeGroupBuilder is null
            ? app
            : routeGroupBuilder;
        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }
        return app;
    }
}
