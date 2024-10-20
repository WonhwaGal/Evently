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
using Evently.Common.Domain;

namespace Evently.Modules.Events.Presentation.Events;

public static class PublishEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("events/publish/{id}", async (Guid id, ISender sender) =>
        {
            var command = new PublishEventCommand(id);
            Result result = await sender.Send(command);

            if (result.IsSuccess)
            {
                return Results.Ok();
            }
            else
            {
                return Results.BadRequest(result.Error);
            }

        }).WithTags(Tags.Events);
    }
}
