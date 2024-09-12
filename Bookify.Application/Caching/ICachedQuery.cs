using Bookify.Application.Abstractions.Messaging;
using System;

namespace Bookify.Application.Caching;

public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery;

public interface ICachedQuery
{
    string CacheKey { get; }

    TimeSpan Expiration { get; }
}
