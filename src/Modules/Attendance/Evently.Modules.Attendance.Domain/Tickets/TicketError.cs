using Evently.Common.Domain;

namespace Evently.Modules.Attendance.Domain.Tickets;

/// <summary>
/// Ошибки, связанные с билетами (Ticket)
/// </summary>
public static class TicketErrors
{
    /// <summary>
    /// Билет с указанным идентификатором не найден
    /// </summary>
    public static readonly Error NotFound = Error.Problem("Tickets.NotFound", "The ticket was not found");

    /// <summary>
    /// Попытка зарегистрировать билет, который не принадлежит мероприятию
    /// </summary>
    public static readonly Error InvalidCheckIn = Error.Problem(
        "Tickets.InvalidCheckIn",
        "The ticket check in was invalid");

    /// <summary>
    /// Попытка зарегистрировать билет, который уже был использован
    /// </summary>
    public static readonly Error DuplicateCheckIn = Error.Problem(
        "Tickets.DuplicateCheckIn",
        "The ticket was already checked in");
}
