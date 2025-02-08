using Evently.Common.Application.Caching;
using Evently.Modules.Events.Application.TicketTypes.GetTicketTypeById;

namespace Evently.Modules.Events.Application.TicketTypes.GetTicketTypes;
public sealed record GetTicketTypesQuery : ICachedQuery<IReadOnlyList<TicketTypeResponse>>
{
    public string CacheKey => $"ticketTypes-all";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
