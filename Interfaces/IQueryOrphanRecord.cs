using Microsoft.Data.SqlClient;

namespace TurboQuery.Interfaces;

public interface IQueryOrphanRecord<T>
{
    Task<T> GetOrphanRecordAsync(string query, Action<SqlCommand> setParameters, Func<SqlDataReader, T> mapFunction);
    T GetOrphanRecord(string query, Action<SqlCommand> setParameters, Func<SqlDataReader, T> mapFunction);
}
