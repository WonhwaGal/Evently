
namespace Evently.Modules.Users.Domain.Users;
public interface IUserRepository
{
    /// <summary>
    /// Получение пользователя по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавление нового пользователя в контекст
    /// </summary>
    /// <param name="user"></param>
    void Insert(User user);
}
