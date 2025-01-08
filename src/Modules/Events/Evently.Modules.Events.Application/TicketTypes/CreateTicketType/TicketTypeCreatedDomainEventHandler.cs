using AutoMapper;
using Evently.Common.Application.EventBus;
using Evently.Common.Application.Exceptions;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.TicketTypes.GetTicketTypeById;
using Evently.Modules.Events.Domain.TicketTypes.TicketTypes;
using Evently.Modules.Events.IntegrationEvents;
using Evently.Modules.Ticketing.PublicApi;
using MediatR;

namespace Evently.Modules.Events.Application.TicketTypes.CreateTicketType;

public class TicketTypeCreatedDomainEventHandler(
    ISender sender,
    IEventBus eventBus,
    IMapper mapper) : IDomainEventHandler<TicketTypeCreatedDomainEvent>
{
    public async Task Handle(TicketTypeCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Result<TicketTypeResponse?> result = await sender.Send(new GetTicketTypeByIdQuery(notification.TicketTypeId), cancellationToken);
        if (result.IsFailure)
        {
            throw new EventlyException(nameof(GetTicketTypeByIdQuery), result.Error);
        }

        TicketTypeCreatedIntegrationEvent integrationEvent = mapper
            .Map<TicketTypeCreatedIntegrationEvent>((notification, result.Value));

        await eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}
