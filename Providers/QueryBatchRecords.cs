using Microsoft.Data.SqlClient;
using System.Data;
using TurboQuery.Interfaces;

namespace TurboQuery.Providers;

public class QueryBatchRecords<T> : BaseTurboQuery, IQueryBatchRecords<T>
{
    /// <summary>
    /// Asynchronously retrieves a paginated subset of data from a database table using a stored procedure.
    /// </summary>
    /// <typeparam name="T">The type of objects to be returned in the result set.</typeparam>
    /// <param name="sql">The SQL query or condition used to filter the data in the table.</param>
    /// <param name="pageNumber">The page number of the data to retrieve (1-based index).</param>
    /// <param name="pageSize">The number of records to include in each page.</param>
    /// <param name="reader">A delegate function that maps a <see cref="SqlDataReader"/> row to an object of type <typeparamref name="T"/>.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an <see cref="IEnumerable{T}"/> 
    /// of objects of type <typeparamref name="T"/> representing the paginated data.
    /// </returns>
    /// <remarks>
    /// This method executes a stored procedure named "SP_TablePagination" to retrieve paginated data from a database table.
    /// The stored procedure is expected to accept three parameters:
    /// - @Query: The SQL query or condition to filter the data.
    /// - @PageNumber: The page number to retrieve.
    /// - @PageSize: The number of records per page.
    /// The <paramref name="reader"/> delegate is used to map each row of the result set to an object of type <typeparamref name="T"/>.
    /// </remarks>
    public async Task<IEnumerable<T>> BatchingTableAsync(string sql, int pageNumber, int pageSize, Func<SqlDataReader, T> reader)
    {

        return await (new QueryExecutor<T>()).ExecuteReaderAsync(ProcedureNameForBatchTable, async (SqlCommand cmd) =>
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Query", sql);
            cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);
            await Task.CompletedTask;
        }, reader);
    }


    /// <summary>
    /// Synchronously retrieves a paginated subset of data from a database table using a stored procedure.
    /// </summary>
    /// <typeparam name="T">The type of objects to be returned in the result set.</typeparam>
    /// <param name="sql">The SQL query or condition used to filter the data in the table.</param>
    /// <param name="pageNumber">The page number of the data to retrieve (1-based index).</param>
    /// <param name="pageSize">The number of records to include in each page.</param>
    /// <param name="reader">A delegate function that maps a <see cref="SqlDataReader"/> row to an object of type <typeparamref name="T"/>.</param>
    /// <returns>
    /// A task that represents the synchronous operation. The task result contains an <see cref="IEnumerable{T}"/> 
    /// of objects of type <typeparamref name="T"/> representing the paginated data.
    /// </returns>
    /// <remarks>
    /// This method executes a stored procedure named <see cref="ProsedureNameForBatchTable"/> to retrieve paginated data from a database table.
    /// The stored procedure is expected to accept three parameters:
    /// - @Query: The SQL query or condition to filter the data.
    /// - @PageNumber: The page number to retrieve.
    /// - @PageSize: The number of records per page.
    /// The <paramref name="reader"/> delegate is used to map each row of the result set to an object of type <typeparamref name="T"/>.
    /// </remarks>
    public IEnumerable<T> BatchingTable(string sql, int pageNumber, int pageSize, Func<SqlDataReader, T> reader)
    {
        return (new QueryExecutor<T>()).ExecuteReader(ProcedureNameForBatchTable, async (SqlCommand cmd) =>
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Query", sql);
            cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);
            await Task.CompletedTask;
        }, reader);
    }
}
