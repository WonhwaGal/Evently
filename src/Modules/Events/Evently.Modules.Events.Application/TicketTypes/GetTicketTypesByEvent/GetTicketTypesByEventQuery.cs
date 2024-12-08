using Evently.Common.Application.Caching;
using Evently.Common.Application.Messaging;
using Evently.Modules.Events.Application.TicketTypes.GetTicketTypeById;

namespace Evently.Modules.Events.Application.TicketTypes.GetByEvent;
public sealed record GetTicketTypesByEventQuery(
    Guid EventId) : ICachedQuery<IReadOnlyList<TicketTypeResponse>>
{
    public string CacheKey => $"tickettypes-{EventId}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(3);
}
