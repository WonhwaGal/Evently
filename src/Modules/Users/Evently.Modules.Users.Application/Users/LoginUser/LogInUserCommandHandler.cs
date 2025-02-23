using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Users.Application.Abstractions.Identity;
using Evently.Modules.Users.Domain.Users;

namespace Evently.Modules.Users.Application.Users.LoginUser;
internal sealed class LogInUserCommandHandler(
    IJwtService jwtService)
    : ICommandHandler<LogInUserCommand, AccessTokenResponse>
{
    public async Task<Result<AccessTokenResponse>> Handle(
        LogInUserCommand request,
        CancellationToken cancellationToken)
    {
        Result<string> result = await jwtService.GetAccessTokenAsync(
            request.Email,
            request.Password,
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
        }

        return new AccessTokenResponse(result.Value);
    }
}
