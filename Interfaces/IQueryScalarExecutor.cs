using Microsoft.Data.SqlClient;

namespace TurboQuery.Interfaces;

public interface IQueryScalarExecutor<T>
{
    Task<T> ExecuteScalarAsync(string Query, Action<SqlCommand> SetParameters);
    T ExecuteScalar(string Query, Action<SqlCommand> SetParameters);
    Task<T> ExecuteScalarAsync(string Query);
    T ExecuteScalar(string Query);
}
