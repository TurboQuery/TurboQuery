using Microsoft.Extensions.DependencyInjection;
using TurboQuery.Interfaces;
using TurboQuery.Providers;

namespace TurboQuery;

public static class ServiceCollectionExtensions
{
 
    public static IServiceCollection AddTurboQuery(this IServiceCollection services, Action<TurboQueryOptions> configureOptions)
    {
        var options = new TurboQueryOptions();
        configureOptions(options);

        TurboQueryGlobals.Configure(options);

        _InitDB();
        return services;
    }

    private static void _InitDB()
    {
        if (!TurboQueryGlobals.IsDbInitialized)
        {
            DatabaseInitializer.InitializeDatabase();
            TurboQueryGlobals.IsDbInitialized = true;
        }
    }
}
