using Microsoft.Extensions.DependencyInjection;
using Pixelbadger.NewsHaiku.Application.Components;
using System.Reflection;

namespace Pixelbadger.NewsHaiku.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        services.AddScoped<IHaikuCache, HaikuCache>();
        services.AddScoped<IImageCache, ImageCache>();

        return services;
    }
}
