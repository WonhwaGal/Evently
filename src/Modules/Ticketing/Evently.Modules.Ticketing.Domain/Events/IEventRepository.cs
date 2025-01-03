
namespace Evently.Modules.Ticketing.Domain.Events;
public interface IEventRepository
{
    /// <summary>
    /// Получить тип билета по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Event?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить тип билета
    /// </summary>
    /// <param name="@event"></param>
    void Insert(Event @event);
}
