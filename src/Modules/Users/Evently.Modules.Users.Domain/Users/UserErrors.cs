using Evently.Common.Domain;

namespace Evently.Modules.Users.Domain.Users;
public static class UserErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("User.NotFound", $"User with identifier {id} wa not found");
}
