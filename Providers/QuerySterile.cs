using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TurboQuery.Providers;

public class QuerySterile : BaseTurboQuery
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
                RowAffected =  cmd.ExecuteNonQuery();
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
}
