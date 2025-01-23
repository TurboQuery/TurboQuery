using Microsoft.Data.SqlClient;

namespace TurboQuery.Interfaces;

public interface IQuerySterile
{
    Task<int> ExecuteNonQueryAsync(string Query, Action<SqlCommand> SetParams);
    int ExecuteNonQuery(string Query, Action<SqlCommand> SetParams);
    Task<int> ExecuteNonQueryAsync(string Query);
    int ExecuteNonQuery(string Query);
    int ExecuteBatchNonQuery<T>(string Query, IEnumerable<T> objects, Action<T, SqlCommand> MapQueryParams);
    Task<int> ExecuteBatchNonQueryAsync<T>(string Query, IEnumerable<T> objects, Action<T, SqlCommand> MapQueryParams);
}
