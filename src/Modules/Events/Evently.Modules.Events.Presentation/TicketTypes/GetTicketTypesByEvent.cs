using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.TicketTypes.GetByEvent;
using Evently.Modules.Events.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.TicketTypes;

public static class GetTicketTypesByEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("ticketTypes/getTicketTypesByEvent", async (Guid eventId, ISender sender) =>
        {
            var query = new GetTicketTypesByEventQuery(eventId);

            Result<IReadOnlyList<TicketTypeResponse>> result = await sender.Send(query);

            if (result.IsSuccess)
            {
                return Results.Ok(result.Value);
            }
            else
            {
                return Results.BadRequest(result.Error);
            }

        }).WithTags(Tags.TicketTypes);
    }
}
