using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Evently.Modules.Events.Application.Events.PublishEvent;

namespace Evently.Modules.Events.Presentation.Events;

public static class PublishEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("events/publish/{id}", async (Guid id, ISender sender) =>
        {
            var command = new PublishEventCommand(id);
            await sender.Send(command);

            return Results.Ok();

        }).WithTags(Tags.Events);
    }
}
