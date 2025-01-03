using Evently.Modules.Ticketing.Domain.Events;
using Evently.Modules.Ticketing.Domain.TicketTypes;
using Evently.Modules.Ticketing.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Ticketing.Infrastructure.TicketTypes;
internal sealed class TicketTypeRepository(TicketingDbContext context) : ITicketTypeRepository
{
    public async Task<TicketType?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.TicketTypes.SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public void Insert(TicketType ticketType)
    {
        context.TicketTypes.Add(ticketType);
    }
}
