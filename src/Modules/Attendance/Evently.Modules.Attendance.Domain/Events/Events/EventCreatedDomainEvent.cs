using Evently.Common.Domain;

namespace Evently.Modules.Attendance.Domain.Events.Events;

/// <summary>
/// Доменное событие, сигнализирующее о том, что мероприятие было создано
/// </summary>
/// <param name="eventId">Идентификатор мероприятия</param>
/// <param name="title">Наименование</param>
/// <param name="description">Описание</param>
/// <param name="location">Место проведения</param>
/// <param name="startsAtUtc">Дата и время начала проведения мероприятия в формате UTC</param>
/// <param name="endsAtUtc">Дата и время окончания проведения мероприятия в формате UTC</param>
public sealed class EventCreatedDomainEvent(
    Guid eventId,
    string title,
    string description,
    string location,
    DateTime startsAtUtc,
    DateTime? endsAtUtc) : DomainEvent
{
    /// <summary>
    /// Идентификатор мероприятия
    /// </summary>
    public Guid EventId { get; init; } = eventId;

    /// <summary>
    /// Наименование
    /// </summary>
    public string Title { get; init; } = title;

    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; init; } = description;

    /// <summary>
    /// Место проведения
    /// </summary>
    public string Location { get; init; } = location;

    /// <summary>
    /// Дата и время начала проведения мероприятия в формате UTC
    /// </summary>
    public DateTime StartsAtUtc { get; init; } = startsAtUtc;

    /// <summary>
    /// Дата и время окончания проведения мероприятия в формате UTC
    /// </summary>
    public DateTime? EndsAtUtc { get; init; } = endsAtUtc;
}
