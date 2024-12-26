using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Application.EventBus;

namespace Evently.Modules.Users.IntegrationEvents;

/// <summary>
/// Интеграционное событие регистрации пользователя
/// </summary>
public sealed class UserRegisteredIntegrationEvent : IntegrationEvent
{

    /// <summary>
    /// Конструктор интеграционного события регистрации пользователя
    /// </summary>
    /// <param name="id"> Идентификатор события </param>
    /// <param name="occurredOnUtc"> Время возникновения события </param>
    /// <param name="userId"> Идентификатор пользователя </param>
    /// <param name="email"> Email пользователя </param>
    /// <param name="firstName"> Имя пользователя </param>
    /// <param name="lastName"> Фамилия пользователя </param>
    public UserRegisteredIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid userId,
        string email,
        string firstName,
        string lastName
    ) : base(id, occurredOnUtc)
    {
        UserId = userId;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public UserRegisteredIntegrationEvent()
    {
    }

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Email пользователя
    /// </summary>
    public string Email { get; init; }

    /// <summary>
    /// Имя и фамилия пользователя
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string LastName { get; init; }
}

