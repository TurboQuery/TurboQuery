namespace TurboQuery;

public abstract class BaseTurboQuery
{
    protected static readonly string ConnectionString = "Server=localhost;Database=PlayStationClub;User Id=sa;Password=sa123456;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";

    protected static readonly string ProcedureNameForBatchTable = "SP_TablePagination";
}
