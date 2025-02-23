using Evently.Common.Domain;

namespace Evently.Modules.Users.Application.Abstractions.Identity;
public interface IJwtService
{
    Task<Result<string>> GetAccessTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);
}
