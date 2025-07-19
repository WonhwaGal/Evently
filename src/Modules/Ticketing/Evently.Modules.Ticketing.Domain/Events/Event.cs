using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Events.Events;

namespace Evently.Modules.Ticketing.Domain.Events;
/// <summary>
/// Сущность "Мероприятие" (Event) отражение сущности
/// "Мероприятие" (Event), Evently.Modules.Events.Domain.Events
/// </summary>
public sealed class Event : Entity
{
    private Event()
    {
    }

    /// <summary>
    ///  Идентификатор мероприятия
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
    /// Дата начала мероприятия
    /// </summary>
    public DateTime StartsAtUtc { get; private set; }

    /// <summary>
    /// Дата окончания мероприятия
    /// </summary>
    public DateTime? EndsAtUtc { get; private set; }

    /// <summary>
    /// Отменено
    /// </summary>
    public bool Canceled { get; private set; }

    /// <summary>
    /// Фабричный метод для создания нового объекта мероприятия
    /// </summary>
    /// <param name="id"> Идентификатор мероприятия </param>
    /// <param name="title"> Наименование </param>
    /// <param name="description"> Описание </param>
    /// <param name="location"> Место проведения </param>
    /// <param name="startsAtUtc"> Дата начала мероприятия </param>
    /// <param name="endsAtUtc"> Дата окончания мероприятия </param>
    /// <returns></returns>
    public static Event Create(
        Guid id,
        string title,
        string description,
        string location,
        DateTime startsAtUtc,
        DateTime? endsAtUtc)
    {
        var @event = new Event
        {
            Id = id,
            Title = title,
            Description = description,
            Location = location,
            StartsAtUtc = startsAtUtc,
            EndsAtUtc = endsAtUtc
        };

        return @event;
    }

    /// <summary>
    /// Обновить данные мероприятия
    /// </summary>
    /// <param name="startsAtUtc"> Дата начала мероприятия </param>
    /// <param name="endsAtUtc"> Дата окончания мероприятия </param>
    public void Reschedule(DateTime startsAtUtc, DateTime? endsAtUtc)
    {
        StartsAtUtc = startsAtUtc;
        EndsAtUtc = endsAtUtc;
        // Возбудить событие переноса мероприятия
        Raise(new EventRescheduledDomainEvent(Id, StartsAtUtc, EndsAtUtc));
    }

    /// <summary>
    /// Отменить мероприятие
    /// </summary>
    public void Cancel()
    {
        // Если мероприятие уже отменено
        if (Canceled)
        {
            // Завершить выполнение метода
            return;
        }
        // Установить флаг отмены мероприятия
        Canceled = true;
        // Возбудить событие отмены мероприятия
        Raise(new EventCanceledDomainEvent(Id));
    }

    /// <summary>
    /// Восстановить мероприятие
    /// </summary>
    public void PaymentsRefunded()
    {
        // Возбудить событие отмены платежа
        Raise(new EventPaymentsRefundedDomainEvent(Id));
    }

    /// <summary>
    /// Архивировать мероприятие
    /// </summary>
    public void TicketsArchived()
    {
        // Возбудить событие архивации билетов
        Raise(new EventTicketsArchivedDomainEvent(Id));
    }
}
