using Evently.Common.Domain;
using Evently.Modules.Attendance.Domain.Events.Events;

namespace Evently.Modules.Attendance.Domain.Events;

/// <summary>
/// Мероприятие (Event)
/// </summary>
public sealed class Event : Entity
{
    /// <summary>
    /// Статический метод для создания нового мероприятия
    /// </summary>
    /// <param name="id"></param>
    /// <param name="title"></param>
    /// <param name="description"></param>
    /// <param name="location"></param>
    /// <param name="startsAtUtc"></param>
    /// <param name="endsAtUtc"></param>
    /// <returns></returns>
    public static Event Create(
        Guid id,
        string title,
        string description,
        string location,
        DateTime startsAtUtc,
        DateTime? endsAtUtc)
    {
        // Создать новое мероприятие
        var @event = new Event
        {
            Id = id,
            Title = title,
            Description = description,
            Location = location,
            StartsAtUtc = startsAtUtc,
            EndsAtUtc = endsAtUtc
        };
    
        // Возбудить событие, сигнализирующее о том, что мероприятие было создано
        @event.Raise(new EventCreatedDomainEvent(
            @event.Id,
            @event.Title,
            @event.Description,
            @event.Location,
            @event.StartsAtUtc,
            @event.EndsAtUtc));

        // Вернуть созданное мероприятие
        return @event;
    }
    
    /// <summary>
    /// Конструктор по умолчанию (для EF Core)
    /// </summary>
    private Event()
    {
    }

    /// <summary>
    /// Идентификатор мероприятия
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Место проведения
    /// </summary>
    public string Location { get; private set; }

    /// <summary>
    /// Дата и время начала проведения мероприятия в формате UTC
    /// </summary>
    public DateTime StartsAtUtc { get; private set; }

    /// <summary>
    /// Дата и время окончания проведения мероприятия в формате UTC
    /// </summary>
    public DateTime? EndsAtUtc { get; private set; }
}
