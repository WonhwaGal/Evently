using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.TicketTypes;
public static class TicketTypeErrors
{
    public static Error NotFound(Guid ticketId) => Error.NotFound(
        "TicketTypeErrors.NotFound",
        $"Ticket Type with the identifier {ticketId} was not found");

    public static Error IncorrentPrice => Error.Problem(
        "TicketTypeErrors.IncorrentPrice",
        "Specified price is not admissible");

}
