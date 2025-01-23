using Microsoft.Extensions.DependencyInjection;
using TurboQuery.Interfaces;
using TurboQuery.Providers.SqlServer;

namespace TurboQuery.Config;

public class DependenciesInjection
{
    public static IServiceCollection AddSqlServerServices(ref IServiceCollection services)
    {
        services.AddScoped(typeof(IQueryBatchRecords<>), typeof(QueryBatchRecords<>));
        services.AddScoped(typeof(IQueryExecutor<>), typeof(QueryExecutor<>));
        services.AddScoped(typeof(IQueryOrphanRecord<>), typeof(QueryOrphanRecord<>));
        services.AddScoped(typeof(IQueryScalarExecutor<>), typeof(QueryScalarExecutor<>));
        services.AddScoped<IQuerySterile, QuerySterile>();
        return services;
    }
}
