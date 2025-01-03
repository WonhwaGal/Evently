using Evently.Common.Domain;

namespace Evently.Modules.Ticketing.Domain.Events;
public sealed class Event: Entity
{
    private Event()
    {
    }

    public static Event Create(
        Guid id,
        Guid categotyId,
        string title,
        string description,
        string location,
        DateTime startAtUtc,
        DateTime? endAtUtc
        )
    {
        var @event = new Event
        {
            Id = id,
            CategoryId = categotyId,
            Title = title,
            Description = description,
            Location = location,
            StartsAtUtc = startAtUtc,
            EndsAtUtc = endAtUtc,
            Status = EventStatus.Draft
        };

        return @event;
    }

    /// <summary>
    /// Мероприятие (концерт, фестиваль, выставка и т.д.)
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор категории мероприятия
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Идентификатор
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Место проведения
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    ///  Дата и время мероприятия
    /// </summary>
    public DateTime StartsAtUtc { get; set; }

    /// <summary>
    /// Дата и время окончания мероприятия
    /// </summary>
    public DateTime? EndsAtUtc { get; set; }

    /// <summary>
    /// Статус мероприятия
    /// </summary>
    public EventStatus Status { get; set; }
}
