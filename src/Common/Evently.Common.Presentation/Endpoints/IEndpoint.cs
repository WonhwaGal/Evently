using Microsoft.AspNetCore.Routing;

namespace Evently.Common.Presentation.Endpoints;

/// <summary>
/// Интерфейс описывающий контракт для регистрации конечной точки
/// </summary>
public interface IEndpoint
{
    /// <summary>
    /// Метод для регистрации конечной точки
    /// </summary>
    /// <param name="app"></param>
    void MapEndpoint(IEndpointRouteBuilder app);
}
