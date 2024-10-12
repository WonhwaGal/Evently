using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.TicketTypes;
public static class TicketTypesEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        CreateTicketType.MapEndpoint(app);
        UpdateTicketTypePrice.MapEndpoint(app);
    }
}
