using AutoMapper;
using Evently.Common.Application.EventBus;
using Evently.Common.Application.Exceptions;
using Evently.Common.Domain;
using Evently.Modules.Ticketing.Application.Customers.CreateCustomer;
using Evently.Modules.Users.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Evently.Modules.Ticketing.Presentation.Customers;
public sealed class UserRegisteredIntegrationEventHandler(
    ISender sender,
    IMapper mapper) : IntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    public override async Task Handle(UserRegisteredIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(mapper.Map<CreateCustomerCommand>(integrationEvent),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new EventlyException(nameof(CreateCustomerCommand), result.Error);
        }
    }
}

//public sealed class UserRegisteredIntegrationEventConsumer(
//    ISender sender,
//    IMapper mapper) : IConsumer<UserRegisteredIntegrationEvent>
//{
//    public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
//    {
//        Result result = await sender.Send(mapper.Map<CreateCustomerCommand>(context.Message),
//            context.CancellationToken);

//        if (result.IsFailure)
//        {
//            throw new EventlyException(nameof(CreateCustomerCommand), result.Error);
//        }
//    }
//}
