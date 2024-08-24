using HtmlAgilityPack;

namespace Pixelbadger.NewsHaiku.Infrastructure;

internal class NewspaperFrontPageScraper : INewspaperFrontPageScraper
{
    private static readonly Uri _baseUri = new Uri("https://www.frontpages.com/");
    private static readonly string[] _papers = [
        "the-sun",
        "daily-mail",
        "the-daily-telegraph",
        "daily-mirror",
        "daily-express",
        "the-guardian",
        "daily-star",
        "financial-times"
    ];

    private readonly HttpClient _httpClient;

    public NewspaperFrontPageScraper()
    {
        _httpClient = new HttpClient() { BaseAddress = _baseUri };
    }

    public async Task<IEnumerable<BinaryData>> ScrapeImageDataAsync()
    {
        List<byte[]> data = new List<byte[]>();

        foreach (string paper in _papers)
        {
            string pageHtml = await _httpClient.GetStringAsync(paper);

            HtmlDocument doc = new();
            doc.LoadHtml(pageHtml);
            HtmlNode imgNode = doc.DocumentNode.SelectSingleNode("//img[@id='giornale-img']");
            string imageUri = imgNode.Attributes["src"].Value;

            byte[] imageData = await _httpClient.GetByteArrayAsync(imageUri);
            data.Add(imageData);
        }

        return data.Select(d => new BinaryData(d));
    }
}
