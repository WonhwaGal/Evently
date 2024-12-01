using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Common.Application.Caching;
public interface ICacheService
{
    /// <summary>
    /// Получить значение из кэша
    /// </summary>
    /// <param name="key"> Ключ </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <typeparam name="T"> Тип значения </typeparam>
    /// <returns> Значение </returns>
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Установить значение в кэш
    /// </summary>
    /// <param name="key"> Ключ </param>
    /// <param name="value"> Значение </param>
    /// <param name="expiration"> Время жизни </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <typeparam name="T"> Тип значения </typeparam>
    /// <returns></returns>
    Task SetAsync<T>(
        string key,
        T value,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить значение из кэша
    /// </summary>
    /// <param name="key"> Ключ </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns></returns>
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
}
