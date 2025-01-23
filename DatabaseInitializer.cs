using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TurboQuery.Config;
using TurboQuery.Providers.SqlServer;

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
        switch (TurboQueryGlobules.Options.DatabaseEngine)
        {
            case Enums.DatabaseEngine.SqlServer:
                DependenciesInjection.AddSqlServerServices(ref services);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(TurboQueryGlobules.Options.DatabaseEngine), TurboQueryGlobules.Options.DatabaseEngine, "Unsupported database engine.");
        }


        return services;
    }
}
