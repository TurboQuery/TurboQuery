using Microsoft.Data.SqlClient;

namespace TurboQuery.Interfaces;

public interface IQueryOrphanRecord
{
    Task<T> GetOrphanRecordAsync<T>(string query, Action<SqlCommand> setParameters, Func<SqlDataReader, T> mapFunction);
    T GetOrphanRecord<T>(string query, Action<SqlCommand> setParameters, Func<SqlDataReader, T> mapFunction);
}
