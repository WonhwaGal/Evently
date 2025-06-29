using Evently.Common.Application.Caching;

namespace Evently.Modules.Users.Application.Users.GetUserById;
public sealed record GetUserByIdQuery(Guid UserId) : ICachedQuery<UserResponse?>
{
    public string CacheKey => $"user-{UserId}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(2);
}
