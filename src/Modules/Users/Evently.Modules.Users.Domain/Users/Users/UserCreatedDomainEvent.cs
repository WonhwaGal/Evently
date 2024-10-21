using Evently.Common.Domain;

namespace Evently.Modules.Users.Domain.Users.Users;
public sealed class UserCreatedDomainEvent(Guid id) : DomainEvent
{
    public Guid UserId { get; set; } = id;
}
