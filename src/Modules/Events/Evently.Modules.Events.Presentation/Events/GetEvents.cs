using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Modules.Events.Application.Events.GetEvents;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;
internal sealed class GetEvents
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("events", async (ISender sender) =>
        {
            var query = new GetEventsQuery();
            Result<IReadOnlyList<EventResponse>> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);

        })
        .WithTags(Tags.Events);
    }
}
