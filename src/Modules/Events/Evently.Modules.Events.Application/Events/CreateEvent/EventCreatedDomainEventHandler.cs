using AutoMapper;
using Evently.Common.Application.EventBus;
using Evently.Common.Application.Exceptions;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Modules.Events.Domain.Events.Events;
using Evently.Modules.Events.IntegrationEvents;
using MediatR;

namespace Evently.Modules.Events.Application.Events.CreateEvent;

public sealed class EventCreatedDomainEventHandler(
    ISender sender,
    IEventBus eventBus,
    IMapper mapper) : DomainEventHandler<EventCreatedDomainEvent>
{
    public override async Task Handle(EventCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Result<EventResponse?> result = await sender.Send(new GetEventQuery(domainEvent.EventId), cancellationToken);
        if(result.IsFailure)
        {
            throw new EventlyException(nameof(GetEventQuery), result.Error);
        }

        EventCreatedIntegrationEvent integrationEvent = mapper
            .Map<EventCreatedIntegrationEvent>((domainEvent, result.Value));
        
        await eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}
