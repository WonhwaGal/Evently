using Evently.Common.Application.Caching;

namespace Evently.Modules.Events.Application.Events.SearchEvents;
public sealed record SearchEventsQuery(
    DateTime? StartDate,
    DateTime? EndDate,
    int Page,
    int PageSize) : ICachedQuery<SearchEventsResponse>
{
    public string CacheKey => $"searchEvents-{StartDate}-{EndDate}-{Page}-{PageSize}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(4);
}
