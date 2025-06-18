using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.Events.RescheduleEvent;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Evently.Common.Presentation.ApiResults;
using Evently.Common.Presentation.Endpoints;

namespace Evently.Modules.Events.Presentation.Events;
public sealed class RescheduleEvent: IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("events/reschedule", async (RescheduleEventRequest request, ISender sender) =>
        {
            var command = new RescheduleEventCommand(
                request.EventId,
                request.NewStartAtUtc,
                request.NewEndAtUtc);

            Result result = await sender.Send(command);

            return result.Match(TypedResults.Ok, ApiResults.Problem);

        }).WithTags(Tags.Events);
    }
}
