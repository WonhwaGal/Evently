using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.PublicApi;
/// <summary>
/// Ответ на запрос типа билета
/// </summary>
/// <param name="TicketTypeId"></param>
/// <param name="Price"></param>
/// <param name="Quantity"></param>
/// <param name="Currency"></param>
public sealed record TicketTypeResponse(
    Guid TicketTypeId,
    decimal Price,
    decimal Quantity,
    string Currency
    );
