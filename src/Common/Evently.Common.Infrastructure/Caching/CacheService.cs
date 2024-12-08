using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Evently.Common.Application.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace Evently.Common.Infrastructure.Caching;
internal sealed class CacheService : ICacheService
{
    // IDistributedCache - интерфейс,
    // описывающий контракт взаимодействия с распределенным кэшем
    private readonly IDistributedCache _cache;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="cache"> Распределенный кэш </param>
    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    /// <summary>
    /// Получить значение из кэша
    /// </summary>
    /// <param name="key"> Ключ </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <typeparam name="T"> Тип значения </typeparam>
    /// <returns> Значение </returns>
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        byte[]? bytes = await _cache.GetAsync(key, cancellationToken);

        return bytes is null ? default : Deserialize<T>(bytes);
    }

    /// <summary>
    /// Удалить значение из кэша
    /// </summary>
    /// <param name="key"> Ключ </param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task RemoveAsync(string key, CancellationToken cancellationToken = default) =>
        _cache.RemoveAsync(key, cancellationToken);

    /// <summary>
    /// Установить значение в кэш
    /// </summary>
    /// <param name="key"> Ключ </param>
    /// <param name="value"> Значение </param>
    /// <param name="expiration"> Время жизни </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <typeparam name="T"> Тип значения </typeparam>
    /// <returns></returns>
    public Task SetAsync<T>(
        string key,
        T value,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default)
    {
        // Сериализация значения
        byte[] bytes = Serialize(value);
        // Установка значения в кэш
        return _cache.SetAsync(key, bytes, CacheOptions.Create(expiration), cancellationToken);
    }

    /// <summary>
    /// Сериализация значения
    /// </summary>
    /// <param name="value"> Значение </param>
    /// <typeparam name="T"> Тип значения </typeparam>
    /// <returns> Значение в виде массива байт </returns>
    private static byte[] Serialize<T>(T value)
    {
        return System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value,
            new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            }));
    }


    /// <summary>
    /// Десериализация значения
    /// </summary>
    /// <param name="bytes"> Массив байт </param>
    /// <typeparam name="T"> Тип значения </typeparam>
    /// <returns> Значение в виде объекта </returns>
    private static T Deserialize<T>(byte[] bytes)
    {
        return JsonConvert.DeserializeObject<T>(System.Text.Encoding.UTF8.GetString(bytes),
            new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            })!;
    }
}
