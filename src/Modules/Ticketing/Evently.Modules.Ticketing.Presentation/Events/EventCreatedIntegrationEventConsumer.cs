using AutoMapper;
using Evently.Common.Application.Exceptions;
using Evently.Common.Domain;
using Evently.Modules.Events.IntegrationEvents;
using Evently.Modules.Ticketing.Application.Events.CreateEvent;
using MassTransit;
using MediatR;

namespace Evently.Modules.Ticketing.Presentation.Events;
public sealed class EventCreatedIntegrationEventConsumer(
    ISender sender,
    IMapper mapper) : IConsumer<EventCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<EventCreatedIntegrationEvent> context)
    {
        Result result = await sender.Send(mapper.Map<CreateEventCommand>(context.Message), 
            context.CancellationToken);

        if (result.IsFailure)
        {
            throw new EventlyException(nameof(CreateEventCommand), result.Error);
        }
    }
}
