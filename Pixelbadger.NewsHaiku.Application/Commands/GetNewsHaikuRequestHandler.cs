using MediatR;
using Pixelbadger.NewsHaiku.Application.Components;
using Pixelbadger.NewsHaiku.Infrastructure;

namespace Pixelbadger.NewsHaiku.Application.Commands;

internal class GetNewsHaikuRequestHandler : IRequestHandler<GetNewsHaikuRequest, string[]>
{
    private readonly INewspaperFrontPageScraper _newspaperFrontPageScraper;
    private readonly IHaikuCache _haikuCache;
    private readonly IImageCache _imageCache;
    private readonly INewsHaikuGenerator _newsHaikuService;

    public GetNewsHaikuRequestHandler(INewspaperFrontPageScraper newspaperFrontPageScraper, IHaikuCache haikuCache, IImageCache imageCache, INewsHaikuGenerator newsHaikuService)
    {
        _newspaperFrontPageScraper = newspaperFrontPageScraper;
        _haikuCache = haikuCache;
        _imageCache = imageCache;
        _newsHaikuService = newsHaikuService;
    }

    public async Task<string[]> Handle(GetNewsHaikuRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<string>? haiku = _haikuCache.Get();

        if (haiku is not null)
        {
            return haiku.ToArray();
        }

        IEnumerable<BinaryData> images = await GetImagesAsync();

        haiku = await _newsHaikuService.GenerateFromImages(images);
        _haikuCache.Put(haiku);

        return haiku.ToArray();

    }

    private async Task<IEnumerable<BinaryData>> GetImagesAsync()
    {
        IEnumerable<BinaryData>? images = _imageCache.Get();
        if (images is not null)
        {
            return images;
        }

        images = await _newspaperFrontPageScraper.ScrapeImageDataAsync();

        // expiry is now + tomorrow's date + 7 hours (so 7am tomorrow...)
        DateTimeOffset expiryTime = DateTimeOffset.UtcNow.AddDays(1).Date.AddHours(7);
        _imageCache.Put(images, expiryTime);

        return images;
    }
}
