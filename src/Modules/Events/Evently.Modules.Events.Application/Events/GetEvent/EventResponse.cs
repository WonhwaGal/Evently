using Evently.Modules.Events.Domain.Events;

namespace Evently.Modules.Events.Application.Events.GetEvent;


public sealed class EventResponse
{
    public EventResponse(
        Guid id,
        string name,
        string description,
        string location,
        DateTime startAtUtc,
        DateTime endAtUtc,
        EventStatus status)
    {
        Id = id;
        Name = name;
        Description = description;
        Location = location;
        StartAtUtc = startAtUtc;
        EndAtUtc = endAtUtc;
        Status = status;
    }

    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор
    /// </summary>
    public string Name { get; set; }

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
    public DateTime EndAtUtc { get; set; }

    /// <summary>
    /// Статус мероприятия
    /// </summary>
    public EventStatus Status { get; set; }
}
