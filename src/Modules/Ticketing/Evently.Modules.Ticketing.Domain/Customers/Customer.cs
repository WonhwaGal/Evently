using Evently.Common.Domain;

namespace Evently.Modules.Ticketing.Domain.Customers;

public sealed class Customer : Entity
{
    private Customer()
    {
    }

    /// <summary>
    /// Идентификатор покупателя
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Электронная почта
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// Фабричный метод для создания нового объекта покупателя
    /// </summary>
    /// <param name="id"> Идентификатор покупателя </param>
    /// <param name="email"> Электронная почта </param>
    /// <param name="firstName"> Имя </param>
    /// <param name="lastName"> Фамилия </param>
    /// <returns></returns>
    public static Customer Create(Guid id, string email, string firstName, string lastName)
    {
        return new Customer
        {
            Id = id,
            Email = email,
            FirstName = firstName,
            LastName = lastName
        };
    }

    /// <summary>
    /// Обновить данные покупателя
    /// </summary>
    /// <param name="firstName"> Имя </param>
    /// <param name="lastName"> Фамилия </param>
    public void Update(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
