using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.TicketTypes.CreateTicketType;
using Evently.Modules.Events.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.TicketTypes;
public static class CreateTicketType
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("ticketTypes/create", async (CreateTicketTypeRequest request, ISender sender) =>
        {
            var command = new CreateTicketTypeCommand(
                request.EventId,
                request.TicketTypeName,
                request.TicketPrice,
                request.Currency,
                request.Quantity);

            Result<Guid> result = await sender.Send(command);

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
