using MediatR;

namespace Evently.Modules.Events.Application.Events.GetEvent;

internal sealed class GetEventQueryHandler : IRequestHandler<GetEventQuery, EventResponse?>
{
    public Task<EventResponse?> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    //public static void MapEndpoint(IEndpointRouteBuilder app)
    //{
    //    app.MapGet("events/{id}", async (Guid id, EventsDbContext context) =>
    //    {
    //        EventResponse? @event = await context.Events
    //                .Where(e => e.Id == id)
    //                .Select(e => new EventResponse(
    //                    e.Id,
    //                    e.Name,
    //                    e.Description,
    //                    e.Location,
    //                    e.StartAtUtc,
    //                    e.EndAtUtc,
    //                    e.Status))
    //                .SingleOrDefaultAsync();

    //        return @event == null ? Results.NotFound() : Results.Ok(@event.Id);
    //    }).WithTags(Tags.Events);
    //}
}
