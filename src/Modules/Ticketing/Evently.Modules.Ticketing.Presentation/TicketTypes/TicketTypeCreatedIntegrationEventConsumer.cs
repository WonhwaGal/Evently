using AutoMapper;
using Evently.Common.Application.Exceptions;
using Evently.Common.Domain;
using Evently.Modules.Events.IntegrationEvents;
using Evently.Modules.Ticketing.Application.TicketTypes.CreateTicketType;
using MassTransit;
using MediatR;

namespace Evently.Modules.Ticketing.Presentation.TicketTypes;
public sealed class TicketTypeCreatedIntegrationEventConsumer(
    ISender sender,
    IMapper mapper) : IConsumer<TicketTypeCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<TicketTypeCreatedIntegrationEvent> context)
    {
        CreateTicketTypeCommand command = mapper.Map<CreateTicketTypeCommand>(context.Message);
        Result result = await sender.Send(command);

        if (result.IsFailure)
        {
            throw new EventlyException(nameof(CreateTicketTypeCommand), result.Error);
        }
    }
}
