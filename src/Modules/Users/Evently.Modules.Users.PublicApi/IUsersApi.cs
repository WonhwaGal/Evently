using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Users.PublicApi;

/// <summary>
/// Контракт описывает публичный API модуля пользователей
/// </summary>
public interface IUsersApi
{
    /// <summary>
    /// Получить пользователя по идентификатору
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Результат выполнения операции </returns>
    Task<UserResponse?> GetAsync(Guid userId, CancellationToken cancellationToken = default);
}
