using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Common.Application.Caching;

namespace Evently.Modules.Events.Application.Events.GetEventsByCategory;
public sealed record GetEventsByCategoryQuery(Guid CategoryId) : ICachedQuery<IReadOnlyList<EventResponse>>
{
    public string CacheKey => $"eventsByCat-{CategoryId}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(4);
}
