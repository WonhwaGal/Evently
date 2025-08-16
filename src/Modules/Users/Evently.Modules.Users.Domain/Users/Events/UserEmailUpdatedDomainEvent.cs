using Evently.Common.Domain;

namespace Evently.Modules.Users.Domain.Users.Events;
public sealed class UserEmailUpdatedDomainEvent(Guid id, string email) : DomainEvent
{
    public Guid UserId { get; set; } = id;
    public string Email { get; set; } = email;
}
