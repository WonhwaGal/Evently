using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Modules.Events.Application.TicketTypes.GetTicketTypeById;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.TicketTypes;
internal static class GetTicketTypeById
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("ticketTypes/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetTicketTypeByIdQuery(id);

            Result<TicketTypeResponse?> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);

        }).WithTags(Tags.TicketTypes);
    }
}
