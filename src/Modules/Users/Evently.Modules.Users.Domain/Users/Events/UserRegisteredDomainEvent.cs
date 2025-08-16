using Evently.Common.Domain;

namespace Evently.Modules.Users.Domain.Users.Events;
public sealed class UserRegisteredDomainEvent(Guid id) : DomainEvent
{
    public Guid UserId { get; set; } = id;
}
