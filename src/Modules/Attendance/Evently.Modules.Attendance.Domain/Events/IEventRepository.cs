namespace Evently.Modules.Attendance.Domain.Events;

/// <summary>
/// Интерфейс репозитория для работы с мероприятиями (Event)
/// </summary>
public interface IEventRepository
{
    /// <summary>
    /// Получить мероприятие (Event) по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор мероприятия</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns></returns>
    Task<Event?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить новое мероприятие (Event)
    /// </summary>
    /// <param name="event">Мероприятие</param>
    void Insert(Event @event);
}
