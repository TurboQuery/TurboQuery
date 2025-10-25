using Microsoft.Data.SqlClient;

namespace TurboQuery.Interfaces;

public interface IQueryBatchRecords
{
    Task<IEnumerable<T>> BatchingTableAsync<T>(string sql, int pageNumber, int pageSize, Func<SqlDataReader, T> reader);
    IEnumerable<T> BatchingTable<T>(string sql, int pageNumber, int pageSize, Func<SqlDataReader, T> reader);
}
