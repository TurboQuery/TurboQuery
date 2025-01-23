using Microsoft.Data.SqlClient;
using TurboQuery.Interfaces;

namespace TurboQuery.Providers.SqlServer;

public class QuerySterile : BaseTurboQuery, IQuerySterile
{
    /// <summary>
    /// Asynchronously executes a SQL query that does not return any result set (e.g., INSERT, UPDATE, DELETE)
    /// and returns the number of rows affected by the operation.
    /// </summary>
    /// <param name="Query">The SQL query or stored procedure to execute.</param>
    /// <param name="SetParams">A delegate that configures the parameters for the <see cref="SqlCommand"/>.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the number of rows affected by the query.
    /// </returns>
    /// <remarks>
    /// This method establishes a connection to the database, configures the SQL command with the provided query and parameters,
    /// executes the command asynchronously, and returns the number of rows affected. The connection is automatically closed
    /// after the operation completes.
    /// </remarks>
    /// <exception cref="SqlException">Thrown when an error occurs during the execution of the SQL command.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the connection string is not valid or the connection cannot be opened.</exception>
    public async Task<int> ExecuteNonQueryAsync(string Query, Action<SqlCommand> SetParams)
    {
        int RowAffected = 0;

        await using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                SetParams(cmd);
                await conn.OpenAsync();
                RowAffected = await cmd.ExecuteNonQueryAsync();
            }
        }
        return RowAffected;
    }


    /// <summary>
    /// Synchronously executes a SQL query that does not return any result set (e.g., INSERT, UPDATE, DELETE)
    /// and returns the number of rows affected by the operation.
    /// </summary>
    /// <param name="Query">The SQL query or stored procedure to execute.</param>
    /// <param name="SetParams">A delegate that configures the parameters for the <see cref="SqlCommand"/>.</param>
    /// <returns>
    /// A task that represents the Synchronous operation. The task result contains the number of rows affected by the query.
    /// </returns>
    /// <remarks>
    /// This method establishes a connection to the database, configures the SQL command with the provided query and parameters,
    /// executes the command Synchronously, and returns the number of rows affected. The connection is automatically closed
    /// after the operation completes.
    /// </remarks>
    /// <exception cref="SqlException">Thrown when an error occurs during the execution of the SQL command.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the connection string is not valid or the connection cannot be opened.</exception>
    public int ExecuteNonQuery(string Query, Action<SqlCommand> SetParams)
    {
        int RowAffected = 0;

        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                SetParams(cmd);
                conn.Open();
                RowAffected = cmd.ExecuteNonQuery();
            }
        }
        return RowAffected;
    }


    /// <summary>
    /// Asynchronously executes a SQL query that does not return any result set (e.g., INSERT, UPDATE, DELETE)
    /// and returns the number of rows affected by the operation.
    /// </summary>
    /// <param name="Query">The SQL query or stored procedure to execute.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the number of rows affected by the query.
    /// </returns>
    /// <remarks>
    /// This method establishes a connection to the database, executes the provided SQL query asynchronously,
    /// and returns the number of rows affected. The connection is automatically closed after the operation completes.
    /// This method does not support parameterized queries. If parameters are required, use the overload that accepts an <see cref="Action{SqlCommand}"/> delegate.
    /// </remarks>
    /// <exception cref="SqlException">Thrown when an error occurs during the execution of the SQL command.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the connection string is not valid or the connection cannot be opened.</exception>
    public async Task<int> ExecuteNonQueryAsync(string Query)
    {
        int RowAffected = 0;

        await using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                await conn.OpenAsync();
                RowAffected = await cmd.ExecuteNonQueryAsync();
            }
        }
        return RowAffected;
    }


    /// <summary>
    /// Synchronously executes a SQL query that does not return any result set (e.g., INSERT, UPDATE, DELETE)
    /// and returns the number of rows affected by the operation.
    /// </summary>
    /// <param name="Query">The SQL query or stored procedure to execute.</param>
    /// <returns>
    /// A task that represents the synchronous operation. The task result contains the number of rows affected by the query.
    /// </returns>
    /// <remarks>
    /// This method establishes a connection to the database, executes the provided SQL query synchronously,
    /// and returns the number of rows affected. The connection is automatically closed after the operation completes.
    /// This method does not support parameterized queries. If parameters are required, use the overload that accepts an <see cref="Action{SqlCommand}"/> delegate.
    /// </remarks>
    /// <exception cref="SqlException">Thrown when an error occurs during the execution of the SQL command.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the connection string is not valid or the connection cannot be opened.</exception>
    public int ExecuteNonQuery(string Query)
    {
        int RowAffected = 0;

        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                conn.Open();
                RowAffected = cmd.ExecuteNonQuery();
            }
        }
        return RowAffected;
    }

    /// <summary>
    /// Executes a batch of parameterized SQL queries for a collection of objects.
    /// </summary>
    /// <typeparam name="T">The type of objects in the collection.</typeparam>
    /// <param name="query">The SQL query to execute. This should be a parameterized query (e.g., "INSERT INTO table (column1, column2) VALUES (@Param1, @Param2)").</param>
    /// <param name="objects">A collection of objects for which the query will be executed. Each object's properties will be mapped to the query parameters.</param>
    /// <param name="mapParameters">
    /// A delegate that maps the properties of each object to the parameters of the SQL query.
    /// This delegate is responsible for adding parameters to the <see cref="SqlCommand"/> using <see cref="SqlCommand.Parameters.AddWithValue"/>.
    /// </param>
    /// <returns>The number of objects processed (i.e., the number of queries executed).</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="query"/>, <paramref name="objects"/>, or <paramref name="mapParameters"/> is null.
    /// </exception>
    /// <exception cref="SqlException">
    /// Thrown if an error occurs while executing the SQL query.
    /// </exception>
    /// <remarks>
    /// This method is useful for executing the same SQL query for multiple objects in a batch. It uses parameterized queries to prevent SQL injection
    /// and ensures that each object's properties are safely mapped to the query parameters.
    /// 
    /// Example usage:
    /// <code>
    /// var users = new List&lt;User&gt;
    /// {
    ///     new User { Id = 1, Username = "Alice" },
    ///     new User { Id = 2, Username = "Bob" }
    /// };
    /// 
    /// int rowsProcessed = ExecuteBatchNonQuery(
    ///     "INSERT INTO users (id, username) VALUES (@Id, @Username);",
    ///     users,
    ///     (user, command) =>
    ///     {
    ///         command.Parameters.AddWithValue("@Id", user.Id);
    ///         command.Parameters.AddWithValue("@Username", user.Username);
    ///     }
    /// );
    /// </code>
    /// </remarks>
    public int ExecuteBatchNonQuery<T>(string Query, IEnumerable<T> objects, Action<T, SqlCommand> MapQueryParams)
    {
        int RowAffected = 0;
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            connection.Open();

            foreach (T obj in objects)
            {
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    MapQueryParams(obj, command);
                    RowAffected += command.ExecuteNonQuery();
                }
            }
        }

        return RowAffected;
    }

    /// <summary>
    /// Executes a batch of parameterized SQL queries for a collection of objects asynchronous.
    /// </summary>
    /// <typeparam name="T">The type of objects in the collection.</typeparam>
    /// <param name="query">The SQL query to execute. This should be a parameterized query (e.g., "INSERT INTO table (column1, column2) VALUES (@Param1, @Param2)").</param>
    /// <param name="objects">A collection of objects for which the query will be executed. Each object's properties will be mapped to the query parameters.</param>
    /// <param name="mapParameters">
    /// A delegate that maps the properties of each object to the parameters of the SQL query.
    /// This delegate is responsible for adding parameters to the <see cref="SqlCommand"/> using <see cref="SqlCommand.Parameters.AddWithValue"/>.
    /// </param>
    /// <returns>The number of objects processed (i.e., the number of queries executed).</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="query"/>, <paramref name="objects"/>, or <paramref name="mapParameters"/> is null.
    /// </exception>
    /// <exception cref="SqlException">
    /// Thrown if an error occurs while executing the SQL query.
    /// </exception>
    /// <remarks>
    /// This method is useful for executing the same SQL query for multiple objects in a batch. It uses parameterized queries to prevent SQL injection
    /// and ensures that each object's properties are safely mapped to the query parameters.
    /// 
    /// Example usage:
    /// <code>
    /// var users = new List&lt;User&gt;
    /// {
    ///     new User { Id = 1, Username = "Alice" },
    ///     new User { Id = 2, Username = "Bob" }
    /// };
    /// 
    /// int rowsProcessed = ExecuteBatchNonQuery(
    ///     "INSERT INTO users (id, username) VALUES (@Id, @Username);",
    ///     users,
    ///     (user, command) =>
    ///     {
    ///         command.Parameters.AddWithValue("@Id", user.Id);
    ///         command.Parameters.AddWithValue("@Username", user.Username);
    ///     }
    /// );
    /// </code>
    /// </remarks>
    public async Task<int> ExecuteBatchNonQueryAsync<T>(string Query, IEnumerable<T> objects, Action<T, SqlCommand> MapQueryParams)
    {
        int RowAffected = 0;
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            await connection.OpenAsync();

            foreach (T obj in objects)
            {
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    MapQueryParams(obj, command);
                    RowAffected += command.ExecuteNonQuery();
                }
            }
        }

        return RowAffected;
    }
}
