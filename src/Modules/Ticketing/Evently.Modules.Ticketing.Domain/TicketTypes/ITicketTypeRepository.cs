
namespace Evently.Modules.Ticketing.Domain.TicketTypes;
public interface ITicketTypeRepository
{
    /// <summary>
    /// Получить тип билета по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TicketType?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить тип билета
    /// </summary>
    /// <param name="ticketType"></param>
    void Insert(TicketType ticketType);
}
