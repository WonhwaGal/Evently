using Evently.Common.Domain;

namespace Evently.Modules.Users.Domain.Users;
public static class UserErrors
{
    public static Error NotFound(Guid id) =>
        new("User.NotFound", $"User with identifier {id} wa not found", ErrorType.NotFound);
}
