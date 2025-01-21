namespace TurboQuery;

public class TurboQueryOptions
{
    public string ConnectionString { get; set; } = "Server=localhost;Database=PlayStationClub;User Id=sa;Password=sa123456;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";
    public string ProcedureNameForBatchTable { get; set; } = "SP_TablePagination";
}
