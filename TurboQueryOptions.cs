namespace TurboQuery;

public class TurboQueryOptions
{
    public string ConnectionString { get; set; }
    public string ProcedureNameForBatchTable { get; set; } = "SP_BatchingRecords";
}
