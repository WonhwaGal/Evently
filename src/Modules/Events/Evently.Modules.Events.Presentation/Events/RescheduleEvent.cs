using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.Events.RescheduleEvent;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;
public static class RescheduleEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("events/reschedule", async (RescheduleEventRequest request, ISender sender) =>
        {
            var command = new RescheduleEventCommand(
                request.EventId,
                request.NewStartAtUtc,
                request.NewEndAtUtc);

            await sender.Send(command);

            return Results.Ok();

        }).WithTags(Tags.Events);
    }
}
