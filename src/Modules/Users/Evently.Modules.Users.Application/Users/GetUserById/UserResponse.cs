
namespace Evently.Modules.Users.Application.Users.GetUserById;
public sealed class UserResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Электронная почта
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string LastName { get; set; }
}
