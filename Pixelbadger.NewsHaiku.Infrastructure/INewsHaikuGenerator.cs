
namespace Pixelbadger.NewsHaiku.Infrastructure;

public interface INewsHaikuGenerator
{
    Task<string[]> GenerateFromImages(IEnumerable<BinaryData> images);
}