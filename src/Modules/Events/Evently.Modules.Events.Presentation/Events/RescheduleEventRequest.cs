namespace Evently.Modules.Events.Presentation.Events;

public sealed class RescheduleEventRequest
{
    /// <summary>
    /// Идентификатор мероприятия
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    ///  Новая дата и время мероприятия
    /// </summary>
    public DateTime? NewStartAtUtc { get; set; }

    /// <summary>
    /// Новая дата и время окончания мероприятия
    /// </summary>
    public DateTime? NewEndAtUtc { get; set; }
}
