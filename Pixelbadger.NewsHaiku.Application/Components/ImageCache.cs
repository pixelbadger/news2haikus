using Microsoft.Extensions.Caching.Memory;

namespace Pixelbadger.NewsHaiku.Application.Components;
internal class ImageCache : IImageCache
{
    private const string CacheKey = "ImageCacheEntry";

    private readonly IMemoryCache _memoryCache;

    public ImageCache(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public IEnumerable<BinaryData>? Get()
    {
        return _memoryCache.Get<IEnumerable<BinaryData>>(CacheKey);
    }

    public void Put(IEnumerable<BinaryData> value, DateTimeOffset expiration)
    {
        _memoryCache.Set(CacheKey, value, expiration);
    }
}
