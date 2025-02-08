using Evently.Common.Application.Caching;
using Evently.Modules.Events.Application.Events.GetEvent;

namespace Evently.Modules.Events.Application.Events.GetEvents;
public sealed record GetEventsQuery : ICachedQuery<IReadOnlyList<EventResponse>>
{
    public string CacheKey => $"events-all";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
