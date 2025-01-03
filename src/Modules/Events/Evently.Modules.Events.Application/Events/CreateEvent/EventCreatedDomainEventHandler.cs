using Evently.Common.Application.Exceptions;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Modules.Events.Domain.Events.Events;
using Evently.Modules.Ticketing.PublicApi;
using MediatR;

namespace Evently.Modules.Events.Application.Events.CreateEvent;

public sealed class EventCreatedDomainEventHandler(
    ISender sender,
    ITicketingApi ticketingApi) : IDomainEventHandler<EventCreatedDomainEvent>
{
    public async Task Handle(EventCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Result<EventResponse?> result = await sender.Send(new GetEventQuery(notification.EventId), cancellationToken);
        if(result.IsFailure)
        {
            throw new EventlyException(nameof(GetEventQuery), result.Error);
        }

        await ticketingApi.CreateEventAsync(result.Value.Id,
            result.Value.CategoryId,
            result.Value.Title,
            result.Value.Description,
            result.Value.Location,
            result.Value.StartsAtUtc,
            result.Value.EndsAtUtc,
            cancellationToken);
    }
}
