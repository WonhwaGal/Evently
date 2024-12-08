using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Common.Application.Caching;

namespace Evently.Modules.Events.Application.Events.GetEvents;
public sealed record GetEventsByPerQuery(
    DateTime? StartsAtUtc,
    DateTime? EndsAtUtc) : ICachedQuery<IReadOnlyList<EventResponse>>
{
    public string CacheKey => $"eventsByPer-{StartsAtUtc}-{EndsAtUtc}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(4);
}
