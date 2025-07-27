namespace Evently.Modules.Attendance.Domain.Events;

/// <summary>
/// Интерфейс репозитория для работы со статистикой мероприятий (EventStatisticsV2)
/// </summary>
public interface IEventStatisticsRepositoryV2
{
    /// <summary>
    /// Получить статистику мероприятия по его идентификатору
    /// </summary>
    /// <param name="eventId">Идентификатор мероприятия</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Статистика мероприятия</returns>
    Task<EventStatisticsV2> GetAsync(Guid eventId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить новую статистику мероприятия
    /// </summary>
    /// <param name="eventStatistics">Статистика мероприятия</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns></returns>
    Task InsertAsync(EventStatisticsV2 eventStatistics, CancellationToken cancellationToken = default);

    /// <summary>
    /// Заменить существующую статистику мероприятия
    /// </summary>
    /// <param name="eventStatistics">Статистика мероприятия</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns></returns>
    Task ReplaceAsync(EventStatisticsV2 eventStatistics, CancellationToken cancellationToken = default);
}
