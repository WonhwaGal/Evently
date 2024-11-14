using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.TicketTypes.GetByEvent;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Evently.Common.Presentation.ApiResults;

namespace Evently.Modules.Events.Presentation.TicketTypes;

public static class GetTicketTypesByEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("ticketTypes/getTicketTypesByEvent", async (Guid eventId, ISender sender) =>
        {
            var query = new GetTicketTypesByEventQuery(eventId);

            Result<IReadOnlyList<TicketTypeResponse>> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);

        }).WithTags(Tags.TicketTypes);
    }
}
