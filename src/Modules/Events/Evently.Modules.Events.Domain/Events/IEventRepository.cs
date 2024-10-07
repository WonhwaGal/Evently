using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Domain.Events;
public interface IEventRepository
{
    /// <summary>
    /// Получить мероприятие по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Event?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить мероприятие
    /// </summary>
    /// <param name="event"></param>
    void Insert(Event @event);
}
