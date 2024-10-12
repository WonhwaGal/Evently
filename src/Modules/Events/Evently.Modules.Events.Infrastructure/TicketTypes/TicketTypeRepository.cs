using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Domain.TicketTypes;
using Evently.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Events.Infrastructure.TicketTypes;

public class TicketTypeRepository(EventsDbContext context) : ITicketTypeRepository
{
    public async Task<TicketType?> GetAsync(Guid ticketId, CancellationToken cancellationToken)
    {
        return await context.TicketTypes.SingleOrDefaultAsync(t => t.Id == ticketId, cancellationToken);
    }

    public void Insert(TicketType ticketType)
    {
        context.TicketTypes.Add(ticketType);
    }
}
