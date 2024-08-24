using Microsoft.Extensions.Caching.Memory;

namespace Pixelbadger.NewsHaiku.Application.Components;

internal class HaikuCache : IHaikuCache
{
    private static readonly TimeSpan _expiration = TimeSpan.FromMinutes(30);
    private const string CacheKey = "HaikuCacheEntry";

    private readonly IMemoryCache _memoryCache;

    public HaikuCache(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public IEnumerable<string>? Get()
    {
        return _memoryCache.Get<IEnumerable<string>>(CacheKey);
    }

    public void Put(IEnumerable<string> value)
    {
        _memoryCache.Set(CacheKey, value, _expiration);
    }
}