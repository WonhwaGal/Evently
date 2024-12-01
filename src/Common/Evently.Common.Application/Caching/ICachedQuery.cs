using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
