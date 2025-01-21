using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TurboQuery.Providers;

public class QueryScalarExecutor<T> : BaseTurboQuery
{
    /// <summary>
    /// Asynchronously executes a SQL query and returns the first column of the first row in the result set as a specified type.
    /// </summary>
    /// <typeparam name="T">The type of the value to be returned.</typeparam>
    /// <param name="Query">The SQL query to execute.</param>
    /// <param name="SetParameters">A delegate that configures the parameters for the <see cref="SqlCommand"/>.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the first column of the first row in the result set, cast to the specified type, or the default value of <typeparamref name="T"/> if the result is null or <see cref="DBNull"/>.</returns>
    /// <remarks>
    /// This method establishes a connection to the database, configures the SQL command using the provided <paramref name="SetParameters"/> delegate,
    /// and executes the query asynchronously. It returns the result of the first column of the first row, converted to the specified type <typeparamref name="T"/>.
    /// If the result is null or <see cref="DBNull"/>, the default value of <typeparamref name="T"/> is returned.
    /// </remarks>
    /// <example>
    /// <code>
    /// var query = "SELECT COUNT(*) FROM Users WHERE IsActive = @IsActive";
    /// int activeUserCount = await ExecuteScalarAsync<int>(query, cmd =>
    /// {
    ///     cmd.Parameters.AddWithValue("@IsActive", true);
    /// });
    /// </code>
    /// </example>
    /// <exception cref="InvalidCastException">Thrown if the result cannot be cast to the specified type <typeparamref name="T"/>.</exception>
    /// <exception cref="SqlException">Thrown if an error occurs during the execution of the SQL command.</exception>
    public async Task<T> ExecuteScalarAsync(string Query, Action<SqlCommand> SetParameters)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                SetParameters(cmd);
                await conn.OpenAsync();
                object ScalerResult = await cmd.ExecuteScalarAsync();
                
                if (ScalerResult != null && ScalerResult != DBNull.Value)
                    return (T)Convert.ChangeType(ScalerResult, typeof(T));
            }
        }
        return default;
    }

    /// <summary>
    /// Synchronously executes a SQL query and returns the first column of the first row in the result set as a specified type.
    /// </summary>
    /// <typeparam name="T">The type of the value to be returned.</typeparam>
    /// <param name="Query">The SQL query to execute.</param>
    /// <param name="SetParameters">A delegate that configures the parameters for the <see cref="SqlCommand"/>.</param>
    /// <returns>A task that represents the synchronous operation. The task result contains the first column of the first row in the result set, cast to the specified type, or the default value of <typeparamref name="T"/> if the result is null or <see cref="DBNull"/>.</returns>
    /// <remarks>
    /// This method establishes a connection to the database, configures the SQL command using the provided <paramref name="SetParameters"/> delegate,
    /// and executes the query synchronously. It returns the result of the first column of the first row, converted to the specified type <typeparamref name="T"/>.
    /// If the result is null or <see cref="DBNull"/>, the default value of <typeparamref name="T"/> is returned.
    /// </remarks>
    /// <exception cref="InvalidCastException">Thrown if the result cannot be cast to the specified type <typeparamref name="T"/>.</exception>
    /// <exception cref="SqlException">Thrown if an error occurs during the execution of the SQL command.</exception>
    public T ExecuteScalar(string Query, Action<SqlCommand> SetParameters)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                SetParameters(cmd);
                conn.Open();
                object ScalerResult = cmd.ExecuteScalar();

                if (ScalerResult != null && ScalerResult != DBNull.Value)
                    return (T)Convert.ChangeType(ScalerResult, typeof(T));
            }
        }
        return default;
    }

    /// <summary>
    /// Asynchronously executes a SQL query and returns the first column of the first row in the result set as a specified type.
    /// </summary>
    /// <typeparam name="T">The type of the value to be returned.</typeparam>
    /// <param name="Query">The SQL query to execute.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the first column of the first row in the result set, cast to the specified type, or the default value of <typeparamref name="T"/> if the result is null or <see cref="DBNull"/>.</returns>
    /// <remarks>
    /// This method establishes a connection to the database, executes the provided SQL query asynchronously, and returns the result of the first column of the first row, converted to the specified type <typeparamref name="T"/>.
    /// If the result is null or <see cref="DBNull"/>, the default value of <typeparamref name="T"/> is returned.
    /// </remarks>
    /// <exception cref="InvalidCastException">Thrown if the result cannot be cast to the specified type <typeparamref name="T"/>.</exception>
    /// <exception cref="SqlException">Thrown if an error occurs during the execution of the SQL command.</exception>
    public async Task<T> ExecuteScalarAsync(string Query)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                await conn.OpenAsync();
                object ScalerResult = await cmd.ExecuteScalarAsync();

                if (ScalerResult != null && ScalerResult != DBNull.Value)
                   return (T)Convert.ChangeType(ScalerResult, typeof(T));
            }
        }
        return default;
    }

    /// <summary>
    /// Synchronously executes a SQL query and returns the first column of the first row in the result set as a specified type.
    /// </summary>
    /// <typeparam name="T">The type of the value to be returned.</typeparam>
    /// <param name="Query">The SQL query to execute.</param>
    /// <returns>A task that represents the synchronous operation. The task result contains the first column of the first row in the result set, cast to the specified type, or the default value of <typeparamref name="T"/> if the result is null or <see cref="DBNull"/>.</returns>
    /// <remarks>
    /// This method establishes a connection to the database, executes the provided SQL query synchronously, and returns the result of the first column of the first row, converted to the specified type <typeparamref name="T"/>.
    /// If the result is null or <see cref="DBNull"/>, the default value of <typeparamref name="T"/> is returned.
    /// </remarks>
    /// <exception cref="InvalidCastException">Thrown if the result cannot be cast to the specified type <typeparamref name="T"/>.</exception>
    /// <exception cref="SqlException">Thrown if an error occurs during the execution of the SQL command.</exception>
    public T ExecuteScalar(string Query)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                 conn.Open();
                object ScalerResult = cmd.ExecuteScalar();

                if (ScalerResult != null && ScalerResult != DBNull.Value)
                    return (T)Convert.ChangeType(ScalerResult, typeof(T));
            }
        }
        return default;
    }
}
