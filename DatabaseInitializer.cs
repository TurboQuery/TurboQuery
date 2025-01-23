using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TurboQuery.Interfaces;
using TurboQuery.Providers;

namespace TurboQuery;

public class DatabaseInitializer : BaseTurboQuery
{
    private static string _GetSqlScript(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        using (var stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream == null)
            {
                throw new FileNotFoundException($"Resource '{resourceName}' not found.");
            }

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }

    public static void InitializeDatabase()
    {
        string sql = _GetSqlScript("TurboQuery.Scripts.setup.sql");

        if (string.IsNullOrEmpty(sql))
            throw new InvalidOperationException("SQL script could not be loaded.");


        (new QuerySterile()).ExecuteNonQuery(sql);
    }

    public static IServiceCollection ChooseDatabase(ref IServiceCollection services)
    {

        services.AddScoped(typeof(IQueryBatchRecords<>), typeof(QueryBatchRecords<>));
        services.AddScoped(typeof(IQueryExecutor<>), typeof(QueryExecutor<>));
        services.AddScoped(typeof(IQueryOrphanRecord<>), typeof(QueryOrphanRecord<>));
        services.AddScoped(typeof(IQueryScalarExecutor<>), typeof(QueryScalarExecutor<>));
        services.AddScoped<IQuerySterile, QuerySterile>();

        return services;
    }
}
