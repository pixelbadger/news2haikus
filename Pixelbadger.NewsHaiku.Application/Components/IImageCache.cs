
namespace Pixelbadger.NewsHaiku.Application.Components;

public interface IImageCache
{
    IEnumerable<BinaryData>? Get();
    void Put(IEnumerable<BinaryData> value, DateTimeOffset expiration);
}