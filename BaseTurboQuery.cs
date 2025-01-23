namespace TurboQuery;

public abstract class BaseTurboQuery
{
    protected static string ConnectionString;
    protected static string ProcedureNameForBatchTable;
    public BaseTurboQuery()
    {
        ConnectionString = TurboQueryGlobals.Options.ConnectionString;
        ProcedureNameForBatchTable = TurboQueryGlobals.Options.ProcedureNameForBatchTable;
    }
}
