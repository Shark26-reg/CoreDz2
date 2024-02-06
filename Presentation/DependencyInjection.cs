using System.Reflection;

namespace Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddCache();
        services.AddLogging(config =>
        {
            config.AddConsole();
            config.AddDebug();
        });

        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();
        return services;
    }

    private static IServiceCollection AddCache(this IServiceCollection services)
    {
        services.AddMemoryCache(options =>
        {
            options.TrackLinkedCacheEntries = true;
            options.TrackStatistics = true;
        });
        return services;
    }
}
