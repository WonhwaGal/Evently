using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Domain.Events;

public sealed class Event
{
    private Event() { }

    public static Event Create(
        string title,
        string description,
        string location,
        DateTime startAtUtc,
        DateTime? endAtUtc
        )
    {
        var @event = new Event
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            Location = location,
            StartAtUtc = startAtUtc,
            EndAtUtc = endAtUtc,
            Status = EventStatus.Draft
        };

        return @event;
    }

    /// <summary>
    /// Мероприятие (концерт, фестиваль, выставка и т.д.)
    /// </summary>
    public Guid Id { get; set; }

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
    public DateTime StartAtUtc { get; set; }

    /// <summary>
    /// Дата и время окончания мероприятия
    /// </summary>
    public DateTime? EndAtUtc { get; set; }

    /// <summary>
    /// Статус мероприятия
    /// </summary>
    public EventStatus Status { get; set; }
}
