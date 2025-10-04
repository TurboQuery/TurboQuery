using Microsoft.Extensions.DependencyInjection;
using TurboQuery.Interfaces;
using TurboQuery.Providers.SqlServer;

namespace TurboQuery.Config;

public class DependenciesInjection
{
    public static IServiceCollection AddSqlServerServices(IServiceCollection services)
    {
        services.AddSingleton(typeof(IQueryBatchRecords<>), typeof(QueryBatchRecords<>));
        services.AddSingleton(typeof(IQueryExecutor<>), typeof(QueryExecutor<>));
        services.AddSingleton(typeof(IQueryOrphanRecord<>), typeof(QueryOrphanRecord<>));
        services.AddSingleton(typeof(IQueryScalarExecutor<>), typeof(QueryScalarExecutor<>));
        services.AddSingleton<IQuerySterile, QuerySterile>();
        return services;
    }
}
