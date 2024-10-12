using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Domain.Abstractions;

namespace Evently.Modules.Events.Domain.TicketTypes;
public static class TicketTypeErrors
{
    public static Error NotFound(Guid ticketId) => Error.NotFound(
    "TicketTypeErrors.NotFound",
        $"Ticket Type with the identifier {ticketId} was not found");

}
