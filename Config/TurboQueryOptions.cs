using TurboQuery.Enums;

namespace TurboQuery.Config;

public class TurboQueryOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public DatabaseEngine DatabaseEngine { get; set; } = DatabaseEngine.SqlServer;
}
