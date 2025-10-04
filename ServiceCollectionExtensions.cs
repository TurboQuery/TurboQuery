﻿using Microsoft.Extensions.DependencyInjection;
using TurboQuery.Config;

namespace TurboQuery;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddTurboQuery(this  IServiceCollection services, Action<TurboQueryOptions> configureOptions)
    {
        var options = new TurboQueryOptions();
        configureOptions(options);
        
        TurboQueryGlobules.Configure(options);

        _InitDB(services);
        return services;
    }

    private static void _InitDB(IServiceCollection services)
    {
        DatabaseInitializer.ChooseDatabase(services);
        if (!TurboQueryGlobules.IsDbInitialized)
        {
            DatabaseInitializer.InitializeDatabase();
            TurboQueryGlobules.IsDbInitialized = true;
        }
    }
}
