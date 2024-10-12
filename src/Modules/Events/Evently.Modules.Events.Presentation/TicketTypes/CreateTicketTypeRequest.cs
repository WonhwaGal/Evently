using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Presentation.TicketTypes;
public sealed class CreateTicketTypeRequest
{
    /// <summary>
    /// Идентификатор мероприятия
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Наименование билета
    /// </summary>
    public string TicketTypeName { get; set; }

    /// <summary>
    /// Стоимость билета
    /// </summary>
    public decimal TicketPrice { get; set; }

    /// <summary>
    /// Валюта билета
    /// </summary>
    public string Currency {  get; set; }

    /// <summary>
    /// Количество билетов
    /// </summary>
    public decimal Quantity { get; set; }
}
