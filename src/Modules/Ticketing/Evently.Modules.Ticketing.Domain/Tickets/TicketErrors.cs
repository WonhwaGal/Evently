using Evently.Common.Domain;

namespace Evently.Modules.Ticketing.Domain.Tickets;

/// <summary>
/// Справочник ошибок сущности "Билет" (Ticket)
/// </summary>
public static class TicketErrors
{
    
    /// <summary>
    /// Ошибка: билет с указанным идентификатором не найден
    /// </summary>
    /// <param name="ticketId"> Идентификатор билета </param>
    /// <returns></returns>
    public static Error NotFound(Guid ticketId) =>
        Error.NotFound("Tickets.NotFound", $"The ticket with the identifier {ticketId} was not found");

    /// <summary>
    /// Ошибка: билет с указанным кодом не найден
    /// </summary>
    /// <param name="code"> Код билета </param>
    /// <returns></returns>
    public static Error NotFound(string code) =>
        Error.NotFound("Tickets.NotFound", $"The ticket with the code {code} was not found");
    
}
