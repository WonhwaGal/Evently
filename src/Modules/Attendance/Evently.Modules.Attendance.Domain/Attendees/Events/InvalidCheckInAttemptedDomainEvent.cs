using Evently.Common.Domain;

namespace Evently.Modules.Attendance.Domain.Attendees.Events;

/// <summary>
/// Событие, возникающее при попытке невалидной регистрации участника на мероприятии
/// </summary>
/// <param name="attendeeId">Идентификатор участника</param>
/// <param name="eventId">Идентификатор мероприятия</param>
/// <param name="ticketId">Идентификатор билета</param>
/// <param name="ticketCode">Код билета</param>
public sealed class InvalidCheckInAttemptedDomainEvent(Guid attendeeId, Guid eventId, Guid ticketId, string ticketCode)
    : DomainEvent
{
    /// <summary>
    /// Идентификатор участника
    /// </summary>
    public Guid AttendeeId { get; init; } = attendeeId;

    /// <summary>
    /// Идентификатор мероприятия
    /// </summary>
    public Guid EventId { get; init; } = eventId;

    /// <summary>
    /// Идентификатор билета
    /// </summary>
    public Guid TicketId { get; init; } = ticketId;

    /// <summary>
    /// Код билета
    /// </summary>
    public string TicketCode { get; init; } = ticketCode;
}
