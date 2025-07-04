using System.Net;
using Evently.Common.Domain;
using Evently.Modules.Users.Application.Abstractions.Identity;
using Evently.Modules.Users.Infrastructure.Identity.Models;
using Microsoft.Extensions.Logging;

namespace Evently.Modules.Users.Infrastructure.Identity;
internal sealed class IdentityProviderService(
    KeyCloakClient keyCloakClient,
    ILogger<IdentityProviderService> logger) : IIdentityProviderService
{
    private const string PasswordCredentialType = "Password";

    public async Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default)
    {
        var userRepresentation = new UserRepresentation(
            user.Email,
            user.Email,
            user.FirstName,
            user.LastName,
            true,
            true,
            [new CredentialRepresentation(PasswordCredentialType, user.Password, false)]);

        try
        {
            string clientId = await keyCloakClient.GetClientId(cancellationToken);

            RoleRepresentation role = await keyCloakClient.GetRoleId("manager", clientId, cancellationToken);

            string identityId = await keyCloakClient.RegisterUserAsync(userRepresentation, cancellationToken);

            bool result = await keyCloakClient.AssignRoleToUser(role, identityId, clientId, cancellationToken);
            
            return identityId;
        }
        catch (HttpRequestException exception) when (exception.StatusCode == HttpStatusCode.Conflict)
        {
            logger.LogError(exception, "User registration failed");

            return Result.Failure<string>(IdentityProviderErrors.EmailIsNotUnique);
        }
    }
}
