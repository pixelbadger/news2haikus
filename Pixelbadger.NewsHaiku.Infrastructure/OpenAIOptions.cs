using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Pixelbadger.NewsHaiku.Infrastructure;

public class OpenAIOptions
{
    private const string ConfigurationSection = "OpenAI";

    public string Model { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;

    public static void Configure(IHostApplicationBuilder hostApplicationBuilder)
    {
        hostApplicationBuilder.Services.Configure<OpenAIOptions>(hostApplicationBuilder.Configuration.GetSection(ConfigurationSection));
    }
}
