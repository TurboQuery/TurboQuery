using TurboQuery.Enums;

namespace TurboQuery.Config;

public class TurboQueryOptions
{
    public string ConnectionString { get; set; }
    public DatabaseEngine DatabaseEngine { get; set; } = DatabaseEngine.SqlServer;
}
