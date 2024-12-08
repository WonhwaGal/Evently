using Evently.Common.Application.Caching;

namespace Evently.Modules.Events.Application.TicketTypes.GetTicketTypeById;
public sealed record GetTicketTypeByIdQuery(Guid Id) : ICachedQuery<TicketTypeResponse?>
{
    public string CacheKey => $"tickettype-{Id}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(3);
}
