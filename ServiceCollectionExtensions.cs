using Microsoft.Extensions.DependencyInjection;

namespace TurboQuery;

public static class ServiceCollectionExtensions
{
 
    public static IServiceCollection AddTurboQuery(this IServiceCollection services, Action<TurboQueryOptions> configureOptions)
    {
        var options = new TurboQueryOptions();
        configureOptions(options);

        TurboQueryGlobals.Configure(options);

        DatabaseInitializer.InitializeDatabase();
        return services;
    }
}
