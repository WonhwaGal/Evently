using AutoMapper;
using Evently.Common.Application.EventBus;
using Evently.Common.Application.Exceptions;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Users.Application.Users.GetUser;
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
        Result<UserResponse?> result = await sender.Send(new GetUserQuery(domainEvent.UserId), cancellationToken);
        if (result.IsFailure)
        {
            throw new EventlyException(nameof(GetUserQuery), result.Error);
        }

        UserRegisteredIntegrationEvent integrationEvent = mapper
            .Map<UserRegisteredIntegrationEvent>((domainEvent, result.Value));

        await eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}
