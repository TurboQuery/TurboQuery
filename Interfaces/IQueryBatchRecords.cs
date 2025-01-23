using Microsoft.Data.SqlClient;

namespace TurboQuery.Interfaces;

public interface IQueryBatchRecords<T>
{
    Task<IEnumerable<T>> BatchingTableAsync(string sql, int pageNumber, int pageSize, Func<SqlDataReader, T> reader);
    IEnumerable<T> BatchingTable(string sql, int pageNumber, int pageSize, Func<SqlDataReader, T> reader);
}
