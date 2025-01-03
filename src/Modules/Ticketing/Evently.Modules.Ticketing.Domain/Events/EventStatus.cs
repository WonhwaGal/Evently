using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Ticketing.Domain.Events;
public enum EventStatus
{
    /// <summary>
    /// Начальное состояние (черновик)
    /// </summary>
    Draft = 0,

    /// <summary>
    /// Опубликованное мероприятие
    /// </summary>
    Published = 1,

    /// <summary>
    /// Мероприятие успешно завершено
    /// </summary>
    Completed = 2,

    /// <summary>
    /// Отмененное событие
    /// </summary>
    Cancelled = 3
}
