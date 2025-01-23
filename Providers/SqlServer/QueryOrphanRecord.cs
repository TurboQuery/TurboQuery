using Microsoft.Data.SqlClient;
using TurboQuery.Interfaces;

namespace TurboQuery.Providers.SqlServer;

public class QueryOrphanRecord<T> : BaseTurboQuery, IQueryOrphanRecord<T>
{
    /// <summary>
    /// Asynchronously retrieves a single record from the database based on the provided query.
    /// This method is designed to fetch orphan records (standalone records) and map them to a specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the record to be returned. This is determined by the mapping function.</typeparam>
    /// <param name="query">The SQL query to execute against the database. This query should return a single record or no records.</param>
    /// <param name="setParameters">A delegate that sets parameters on the <see cref="SqlCommand"/> before execution. This is used for parameterized queries to prevent SQL injection.</param>
    /// <param name="mapFunction">A delegate that maps the data from the <see cref="SqlDataReader"/> to an object of type <typeparamref name="T"/>.</param>
    /// <returns>A <see cref="Task{T}"/> representing the asynchronous operation. The result is the mapped record of type <typeparamref name="T"/>, or the default value of <typeparamref name="T"/> if no record is found.</returns>
    public async Task<T> GetOrphanRecordAsync(string query, Action<SqlCommand> setParameters, Func<SqlDataReader, T> mapFunction)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                setParameters(cmd);
                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        return mapFunction(reader);
                    }
                }
            }
        }

        return default;
    }

    /// <summary>
    /// Synchronously retrieves a single record from the database based on the provided query.
    /// This method is designed to fetch orphan records (standalone records) and map them to a specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the record to be returned. This is determined by the mapping function.</typeparam>
    /// <param name="query">The SQL query to execute against the database. This query should return a single record or no records.</param>
    /// <param name="setParameters">A delegate that sets parameters on the <see cref="SqlCommand"/> before execution. This is used for parameterized queries to prevent SQL injection.</param>
    /// <param name="mapFunction">A delegate that maps the data from the <see cref="SqlDataReader"/> to an object of type <typeparamref name="T"/>.</param>
    /// <returns>A <see cref="Task{T}"/> representing the synchronous operation. The result is the mapped record of type <typeparamref name="T"/>, or the default value of <typeparamref name="T"/> if no record is found.</returns>
    public T GetOrphanRecord(string query, Action<SqlCommand> setParameters, Func<SqlDataReader, T> mapFunction)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                setParameters(cmd);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return mapFunction(reader);
                    }
                }
            }
        }

        return default;
    }
}
