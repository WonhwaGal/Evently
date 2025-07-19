
namespace Evently.Modules.Ticketing.Domain.TicketTypes;
/// <summary>
/// Интерфейс описывает контракт репозитория типов билетов
/// </summary>
public interface ITicketTypeRepository
{

    /// <summary>
    /// Получить тип билета по идентификатору
    /// </summary>
    /// <param name="id"> Идентификатор типа билета </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns></returns>
    Task<TicketType?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить тип билета по идентификатору с блокировкой
    /// </summary>
    /// <param name="id"> Идентификатор типа билета </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns></returns>
    Task<TicketType?> GetWithLockAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить коллекцию типов билетов
    /// </summary>
    /// <param name="ticketTypes"> Типы билетов </param>
    void InsertRange(IEnumerable<TicketType> ticketTypes);

}
