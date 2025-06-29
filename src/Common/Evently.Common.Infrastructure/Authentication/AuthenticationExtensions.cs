using Microsoft.Extensions.DependencyInjection;

namespace Evently.Common.Infrastructure.Authentication;
/// <summary>
/// Методы расширения для настройки аутентификации
/// </summary>
internal static class AuthenticationExtensions
{
    internal static IServiceCollection AddAuthenticationInternal(this IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddAuthentication()
            .AddJwtBearer();


        services.ConfigureOptions<JwtBearerConfigureOptions>();

        return services;
    }
}
