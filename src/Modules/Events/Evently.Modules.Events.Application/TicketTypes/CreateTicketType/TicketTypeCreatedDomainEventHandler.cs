using AutoMapper;
using Evently.Common.Application.EventBus;
using Evently.Common.Application.Exceptions;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.TicketTypes.GetTicketTypeById;
using Evently.Modules.Events.Domain.TicketTypes.TicketTypes;
using Evently.Modules.Events.IntegrationEvents;
using MediatR;

namespace Evently.Modules.Events.Application.TicketTypes.CreateTicketType;

public class TicketTypeCreatedDomainEventHandler(
    ISender sender,
    IEventBus eventBus,
    IMapper mapper) : DomainEventHandler<TicketTypeCreatedDomainEvent>
{
    public override async Task Handle(TicketTypeCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Result<TicketTypeResponse?> result = await sender.Send(new GetTicketTypeByIdQuery(domainEvent.TicketTypeId), cancellationToken);
        if (result.IsFailure)
        {
            throw new EventlyException(nameof(GetTicketTypeByIdQuery), result.Error);
        }

        TicketTypeCreatedIntegrationEvent integrationEvent = mapper
            .Map<TicketTypeCreatedIntegrationEvent>((domainEvent, result.Value));

        await eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}
