namespace TurboQuery.Config;

internal static class TurboQueryGlobules
{
    public static TurboQueryOptions Options { get; private set; }

    public static bool IsDbInitialized = false;

    // TODO: Make it configurable
    public static string ProcedureNameForBatchTable = "TurboQuery.SP_BatchingRecords";
    
    public static void Configure(TurboQueryOptions options)
    {
        Options = options ?? throw new ArgumentNullException(nameof(options));
    }
}
