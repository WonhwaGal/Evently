using Evently.Common.Domain;

namespace Evently.Modules.Attendance.Domain.Attendees;

/// <summary>
/// Ошибки, связанные с посетителями (Attendee)
/// </summary>
public static class AttendeeErrors
{
    /// <summary>
    /// Посетитель (Attendee) с указанным идентификатором не найден
    /// </summary>
    /// <param name="attendeeId">Идентификатор посетителя</param>
    /// <returns></returns>
    public static Error NotFound(Guid attendeeId) =>
        Error.NotFound("Attendees.NotFound", $"The attendee with the identifier {attendeeId} was not found");
}
