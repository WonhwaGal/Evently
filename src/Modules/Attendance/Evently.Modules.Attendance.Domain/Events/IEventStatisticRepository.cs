namespace Evently.Modules.Attendance.Domain.Events;

/// <summary>
/// Интерфейс репозитория для работы со статистикой мероприятий (EventStatistics)
/// </summary>
public interface IEventStatisticRepository
{
    Task<EventStatistics?> GetAsync(Guid eventId, CancellationToken cancellationToken = default);

    void Insert(EventStatistics eventStatistics);
}
