using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.TicketTypes;
public static class UpdateTicketTypePrice
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("ticketTypes/updatePrice", async (Guid id, decimal price, ISender sender) =>
        {
            var command = new UpdateTicketTypePriceCommand(id, price);

            Result result = await sender.Send(command);

            if (result.IsSuccess)
            {
                return Results.Ok();
            }
            else
            {
                return Results.BadRequest(result.Error);
            }

        }).WithTags(Tags.TicketTypes);
    }
}
