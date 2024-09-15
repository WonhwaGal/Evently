using Evently.Modules.Events.Domain.Events;

namespace Evently.Modules.Events.Application.Events.GetEvent;


public sealed class EventResponse
{
    //public EventResponse(
    //    Guid id,
    //    string title,
    //    string description,
    //    string location,
    //    DateTime startsAtUtc,
    //    DateTime endsAtUtc,
    //    EventStatus status)
    //{
    //    Id = id;
    //    Title = title;
    //    Description = description;
    //    Location = location;
    //    StartsAtUtc = startsAtUtc;
    //    EndsAtUtc = endsAtUtc;
    //    Status = status;
    //}

    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование мероприятия
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Описание мероприятия
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
    public DateTime EndsAtUtc { get; set; }

    /// <summary>
    /// Статус мероприятия
    /// </summary>
    public EventStatus Status { get; set; }
}
