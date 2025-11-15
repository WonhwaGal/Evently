using Evently.Common.Application.Messaging;

namespace Evently.Common.Application.Caching;

public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery;

public interface ICachedQuery
{
    /// <summary>
    /// Ключ кэша
    /// </summary>
    string CacheKey { get; }

    /// <summary>
    /// Время жизни кэша
    /// </summary>
    TimeSpan? Expiration { get; }
}
