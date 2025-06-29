using AutoMapper;
using Evently.Common.Application.EventBus;
using Evently.Common.Application.Exceptions;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Ticketing.PublicApi;
using Evently.Modules.Users.Application.Users.GetUserById;
using Evently.Modules.Users.Domain.Users.Users;
using Evently.Modules.Users.IntegrationEvents;
using MediatR;

namespace Evently.Modules.Users.Application.Users.RegisterUser;

internal sealed class UserCreatedDomainEventHandler(
    ISender sender,
    IEventBus eventBus,
    IMapper mapper) : DomainEventHandler<UserRegisteredDomainEvent>
{
    public override async Task Handle(UserRegisteredDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Result<UserResponse?> result = await sender.Send(new GetUserByIdQuery(domainEvent.UserId), cancellationToken);
        if (result.IsFailure)
        {
            throw new EventlyException(nameof(GetUserByIdQuery), result.Error);
        }

        UserRegisteredIntegrationEvent integrationEvent = mapper
            .Map<UserRegisteredIntegrationEvent>((domainEvent, result.Value));

        await eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}

//internal sealed class UserCreatedDomainEventHandler(
//    ISender sender,
//    ITicketingApi ticketingApi) : IDomainEventHandler<UserRegisteredDomainEvent>
//{
//    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
//    {
//        // Получить информацию о пользователе
//        Result<UserResponse?> result = await sender.Send(new GetUserQuery(notification.UserId), cancellationToken);
//        if (result.IsFailure)
//        {
//            throw new EventlyException(nameof(GetUserQuery), result.Error);
//        }

//        // Создать нового пользователя в модуле Ticketing
//        await ticketingApi.CreateCustomerAsync(
//            notification.UserId,
//            result.Value.Email,
//            result.Value.FirstName,
//            result.Value.LastName,
//            cancellationToken);

//    }
//}
