namespace Evently.Modules.Events.Presentation.Events;

public sealed class CreateEventRequest
{
    /// <summary>
    /// Наименование
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Описание
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
}
