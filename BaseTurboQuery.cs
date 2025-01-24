using TurboQuery.Config;

namespace TurboQuery;

public abstract class BaseTurboQuery
{
    protected static string ConnectionString;
    protected static string ProcedureNameForBatchTable;
    public BaseTurboQuery()
    {
        ConnectionString = TurboQueryGlobules.Options.ConnectionString;
        ProcedureNameForBatchTable = TurboQueryGlobules.ProcedureNameForBatchTable;
    }
}
