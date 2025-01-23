using Microsoft.Data.SqlClient;

namespace TurboQuery.Interfaces;

public interface IQueryExecutor<T>
{
    Task<IEnumerable<T>> ExecuteReaderAsync(string Query, Func<SqlDataReader, T> mapFunction);
    IEnumerable<T> ExecuteReader(string Query, Func<SqlDataReader, T> mapFunction);
    Task<IEnumerable<T>> ExecuteReaderAsync(string Query, Action<SqlCommand> SetParameters, Func<SqlDataReader, T> MapFunction);
    IEnumerable<T> ExecuteReader(string Query, Action<SqlCommand> SetParameters, Func<SqlDataReader, T> MapFunction);
}
