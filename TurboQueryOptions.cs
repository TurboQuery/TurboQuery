using TurboQuery.Enums;

namespace TurboQuery;

public class TurboQueryOptions
{
    public string ConnectionString { get; set; }
    public string ProcedureNameForBatchTable { get; set; } = "SP_BatchingRecords";
    public DatabaseEngine DatabaseEngine { get; set; } = DatabaseEngine.SqlServer;
}
