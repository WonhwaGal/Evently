using Evently.Common.Application.Caching;
using Evently.Modules.Users.Application.Users.GetUser;

namespace Evently.Modules.Users.Application.Users.GetUsers;
public sealed record GetUsersQuery : ICachedQuery<IReadOnlyList<UserResponse>>
{
    public string CacheKey => $"users-all";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
