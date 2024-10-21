using Evently.Common.Domain;

namespace Evently.Modules.Users.Domain.Users.Users;
public sealed class UserFullNameUpdatedDomainEvent(Guid id, string firstName, string lastName) : DomainEvent
{
    public Guid UserId { get; set; } = id; 
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
}
