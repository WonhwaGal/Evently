using System.Net.Http.Json;
using Evently.Common.Domain;
using Evently.Modules.Users.Application.Abstractions.Identity;
using Evently.Modules.Users.Infrastructure.Identity.Models;
using Microsoft.Extensions.Options;

namespace Evently.Modules.Users.Infrastructure.Identity;
internal sealed class JwtService : IJwtService
{
    private static readonly Error AuthenticationFailed = new(
        "Keycloak.AuthenticationFailed",
        "Failed to acquire access token do to authentication failure", ErrorType.Failure);

    private readonly HttpClient _httpClient;
    private readonly KeyCloakOptions _keycloakOptions;

    public JwtService(HttpClient httpClient, IOptions<KeyCloakOptions> keycloakOptions)
    {
        _httpClient = httpClient;
        _keycloakOptions = keycloakOptions.Value;
    }

    public async Task<Result<string>> GetAccessTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var authRequestParameters = new KeyValuePair<string, string>[]
            {
                new("client_id", _keycloakOptions.PublicClientId),
                new("scope", "openid email"),
                new("grant_type", "password"),
                new("username", email),
                new("password", password)
            };

            using var authorizationRequestContent = new FormUrlEncodedContent(authRequestParameters);

            HttpResponseMessage response = await _httpClient.PostAsync("", authorizationRequestContent, cancellationToken);

            response.EnsureSuccessStatusCode();

            AuthorizationToken? authorizationToken = await response.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken);

            if (authorizationToken is null)
            {
                return Result.Failure<string>(AuthenticationFailed);
            }

            return authorizationToken.AccessToken;
        }
        catch (HttpRequestException)
        {
            return Result.Failure<string>(AuthenticationFailed);
        }
    }
}
