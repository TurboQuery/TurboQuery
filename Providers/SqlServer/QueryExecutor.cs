using Microsoft.Data.SqlClient;
using TurboQuery.Interfaces;

namespace TurboQuery.Providers.SqlServer;

public class QueryExecutor<T> : BaseTurboQuery, IQueryExecutor<T>
{
    /// <summary>
    /// Asynchronously executes a SQL query and maps the results to a collection of objects using the provided mapping logic.
    /// </summary>
    /// <typeparam name="T">The type of objects to be returned in the collection.</typeparam>
    /// <param name="Query">The SQL query to execute.</param>
    /// <param name="mapFunction">A delegate function that maps a <see cref="SqlDataReader"/> row to an object of type <typeparamref name="T"/>.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of objects of type <typeparamref name="T"/>.</returns>
    /// <remarks>
    /// This method establishes a connection to the database, executes the provided SQL query, and uses the <paramref name="mapFunction"/> delegate
    /// to transform each row of the result set into an object of type <typeparamref name="T"/>. The method is asynchronous and uses
    /// <see cref="SqlConnection"/>, <see cref="SqlCommand"/>, and <see cref="SqlDataReader"/> for database operations.
    /// </remarks>
    public async Task<IEnumerable<T>> ExecuteReaderAsync(string Query, Func<SqlDataReader, T> mapFunction)
    {
        List<T> Records = new List<T>();
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        T record = mapFunction(reader);
                        Records.Add(record);
                    }
                }
            }
        }
        return Records;
    }

    /// <summary>
    /// synchronously executes a SQL query and maps the results to a collection of objects using the provided mapping logic.
    /// </summary>
    /// <typeparam name="T">The type of objects to be returned in the collection.</typeparam>
    /// <param name="Query">The SQL query to execute.</param>
    /// <param name="mapFunction">A delegate function that maps a <see cref="SqlDataReader"/> row to an object of type <typeparamref name="T"/>.</param>
    /// <returns>A task that represents the synchronously operation. The task result contains a collection of objects of type <typeparamref name="T"/>.</returns>
    /// <remarks>
    /// This method establishes a connection to the database, executes the provided SQL query, and uses the <paramref name="mapFunction"/> delegate
    /// to transform each row of the result set into an object of type <typeparamref name="T"/>. The method is asynchronous and uses
    /// <see cref="SqlConnection"/>, <see cref="SqlCommand"/>, and <see cref="SqlDataReader"/> for database operations.
    /// </remarks>
    public IEnumerable<T> ExecuteReader(string Query, Func<SqlDataReader, T> mapFunction)
    {
        List<T> Records = new List<T>();
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T record = mapFunction(reader);
                        Records.Add(record);
                    }
                }
            }
        }
        return Records;
    }


    /// <summary>
    /// Asynchronously executes a SQL query and processes the resulting data using the provided logic.
    /// This method is designed to retrieve multiple records from a database and map them to a specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the records to be returned. This is determined by the logic function.</typeparam>
    /// <param name="Query">The SQL query to execute against the database. This query can return multiple records.</param>
    /// <param name="SetParameters">
    /// A delegate that asynchronously sets parameters on the <see cref="SqlCommand"/> before execution. 
    /// This is used for parameterized queries to prevent SQL injection.
    /// </param>
    /// <param name="MapFunction">
    /// A delegate that maps the data from the <see cref="SqlDataReader"/> to an object of type <typeparamref name="T"/>.
    /// This is called for each record in the result set.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation. 
    /// The result is an <see cref="IEnumerable{T}"/> containing the mapped records of type <typeparamref name="T"/>.
    /// </returns>
    /// <remarks>
    /// This method uses an asynchronous pattern for database access to ensure scalability and responsiveness. 
    /// Make sure the connection string is properly secured and appropriate for your environment.
    /// </remarks>

    public async Task<IEnumerable<T>> ExecuteReaderAsync(string Query, Action<SqlCommand> SetParameters, Func<SqlDataReader, T> MapFunction)
    {
        List<T> Records = new List<T>();
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                SetParameters(cmd);
                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        T record = MapFunction(reader);
                        Records.Add(record);
                    }
                }
            }
        }
        return Records;
    }

    /// <summary>
    /// Synchronously executes a SQL query and processes the resulting data using the provided logic.
    /// This method is designed to retrieve multiple records from a database and map them to a specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the records to be returned. This is determined by the logic function.</typeparam>
    /// <param name="Query">The SQL query to execute against the database. This query can return multiple records.</param>
    /// <param name="SetParameters">
    /// A delegate that synchronously sets parameters on the <see cref="SqlCommand"/> before execution. 
    /// This is used for parameterized queries to prevent SQL injection.
    /// </param>
    /// <param name="MapFunction">
    /// A delegate that maps the data from the <see cref="SqlDataReader"/> to an object of type <typeparamref name="T"/>.
    /// This is called for each record in the result set.
    /// </param>
    /// <returns>
    /// A task representing the synchronous operation. 
    /// The result is an <see cref="IEnumerable{T}"/> containing the mapped records of type <typeparamref name="T"/>.
    /// </returns>
    /// <remarks>
    /// This method uses an synchronous pattern for database access to ensure scalability and responsiveness. 
    /// Make sure the connection string is properly secured and appropriate for your environment.
    /// </remarks>
    public IEnumerable<T> ExecuteReader(string Query, Action<SqlCommand> SetParameters, Func<SqlDataReader, T> MapFunction)
    {
        List<T> Records = new List<T>();
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                SetParameters(cmd);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T record = MapFunction(reader);
                        Records.Add(record);
                    }
                }
            }
        }
        return Records;
    }
}
