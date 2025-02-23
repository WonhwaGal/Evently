using Evently.Common.Application.Messaging;

namespace Evently.Modules.Users.Application.Users.LoginUser;
public sealed record LogInUserCommand(string Email, string Password)
    : ICommand<AccessTokenResponse>;
