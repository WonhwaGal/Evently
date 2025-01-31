using System.Globalization;
using Evently.Common.Domain;
using Evently.Modules.Users.Domain.Users.Users;

namespace Evently.Modules.Users.Domain.Users;

public sealed class User : Entity
{
    private User() { }

    public static User Create(string email, string firstName, string lastName)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            FirstName = firstName,
            LastName = lastName
        };

        user.Raise(new UserRegisteredDomainEvent(user.Id));

        return user;
    }

    public static User Create(string email, string firstName, string lastName, string identityId)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            IdentityId = identityId
        };

        user.Raise(new UserRegisteredDomainEvent(user.Id));

        return user;
    }

    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Электронная почта
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// Идентификатор пользователя в системе Identity
    /// </summary>
    public string IdentityId { get; private set; }

    public void UpdateEmail(string email)
    {
        if (Email == email)
        {
            return;
        }

        Email = email;

        Raise(new UserEmailUpdatedDomainEvent(Id, Email));
    }

    public void UpdateUserName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;

        Raise(new UserFullNameUpdatedDomainEvent(Id, FirstName, LastName));
    }
}
