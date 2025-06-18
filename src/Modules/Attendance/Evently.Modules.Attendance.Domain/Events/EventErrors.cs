using Evently.Common.Domain;

namespace Evently.Modules.Attendance.Domain.Events;

/// <summary>
/// Ошибки, связанные с мероприятиями (Event)
/// </summary>
public static class EventErrors
{
    /// <summary>
    /// Мероприятие (Event) с указанным идентификатором не найдено
    /// </summary>
    /// <param name="eventId">Идентификатор мероприятия</param>
    /// <returns></returns>
    public static Error NotFound(Guid eventId) =>
        Error.NotFound("Events.NotFound", $"The event with the identifier {eventId} was not found");
}
