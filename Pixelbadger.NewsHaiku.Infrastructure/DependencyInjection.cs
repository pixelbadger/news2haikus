using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenAI;
using System.ClientModel;

namespace Pixelbadger.NewsHaiku.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(sp => new OpenAIClient(new ApiKeyCredential(sp.GetRequiredService<IOptions<OpenAIOptions>>().Value.ApiKey)));
        services.AddScoped<INewsHaikuGenerator, OpenAINewsHaikuGenerator>();
        services.AddScoped<INewspaperFrontPageScraper, NewspaperFrontPageScraper>();

        return services;
    }
}
