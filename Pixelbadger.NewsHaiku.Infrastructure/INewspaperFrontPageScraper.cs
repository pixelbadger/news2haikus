
namespace Pixelbadger.NewsHaiku.Infrastructure;

public interface INewspaperFrontPageScraper
{
    Task<IEnumerable<BinaryData>> ScrapeImageDataAsync();
}