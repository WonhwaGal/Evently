using Evently.Common.Application.Caching;

namespace Evently.Modules.Events.Application.Events.GetEvent;
public sealed record GetEventQuery(Guid EventId) : ICachedQuery<EventResponse?>
{
    public string CacheKey => $"event-{EventId}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(2);
}
