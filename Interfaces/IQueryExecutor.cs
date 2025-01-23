using Microsoft.Data.SqlClient;

namespace TurboQuery.Interfaces;

public interface IQueryExecutor
{
    Task<IEnumerable<T>> ExecuteReaderAsync<T>(string Query, Func<SqlDataReader, T> mapFunction);
    IEnumerable<T> ExecuteReader<T>(string Query, Func<SqlDataReader, T> mapFunction);
    Task<IEnumerable<T>> ExecuteReaderAsync<T>(string Query, Action<SqlCommand> SetParameters, Func<SqlDataReader, T> MapFunction);
    IEnumerable<T> ExecuteReader<T>(string Query, Action<SqlCommand> SetParameters, Func<SqlDataReader, T> MapFunction);
}
