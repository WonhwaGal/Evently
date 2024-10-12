using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Domain.Events;

namespace Evently.Modules.Events.Domain.TicketTypes;
public interface ITicketTypeRepository
{
    /// <summary>
    /// Получить тип билета по идентификатору
    /// </summary>
    /// <param name="ticketId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TicketType?> GetAsync(Guid ticketId, CancellationToken cancellationToken);

    /// <summary>
    /// Добавление нового типа билета
    /// </summary>
    /// <param name="ticketType"></param>
    void Insert(TicketType ticketType);
}
