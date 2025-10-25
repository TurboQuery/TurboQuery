using Microsoft.Data.SqlClient;

namespace TurboQuery.Interfaces;

public interface IQueryScalarExecutor
{
    Task<T> ExecuteScalarAsync<T>(string Query, Action<SqlCommand> SetParameters);
    T ExecuteScalar<T>(string Query, Action<SqlCommand> SetParameters);
    Task<T> ExecuteScalarAsync<T>(string Query);
    T ExecuteScalar<T>(string Query);
}
