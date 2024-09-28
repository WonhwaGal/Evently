using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.Events.CancelEvent;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Evently.Modules.Events.Presentation.Events;

public static class CancelEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("events/cancel/{id}", async (Guid id, ISender sender) =>
        {
            var command = new CancelEventCommand(id);
            await sender.Send(command);

            return Results.Ok();

        }).WithTags(Tags.Events);
    }
}
