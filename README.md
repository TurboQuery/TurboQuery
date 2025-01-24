
# Turbo Query

TurboQuery is a .NET library that streamlines database query execution. It offers a collection of classes and methods to efficiently manage query execution, batch processing, and scalar queries using the ADO package.

## Table of Contents

1. [Installation](#installation)
2. [Configuration](#configuration)
3. [Classes and Methods Documentation](#classes-and-methods-documentation)
   - [QueryExecutor](#queryexecutor)
   - [QueryBatchRecords](#querybatchrecords)
   - [QueryOrphanRecord](#queryorphanrecord)
   - [QueryScalarExecutor](#queryscalarexecutor)
   - [QuerySterile](#querysterile)
   - [DataMapper](#datamapper)
4. [More Details](#moredetails)
5. [Contributing](#contributing)
6. [License](#license)

---

## Installation

To install the package, use the following NuGet command:

```bash
dotnet add package TurboQuery
```

Or, you can install it via the NuGet Package Manager in Visual Studio.


---

## Configuration

```csharp
builder.Services.AddTurboQuery(options =>
{
    options.ConnectionString = "YourConnectionString";
    options.DatabaseEngine = TurboQuery.Enums.DatabaseEngine.SqlServer; // Default value
});
```

---

## Classes and Methods Documentation

Below is the documentation for each class and its methods in the package.



### QueryExecutor


#### `QueryExecutor<T> Class`

The `QueryExecutor<T>` class is a versatile utility designed to execute SQL queries and map the results to a collection of objects of type `T`. It supports both synchronous and asynchronous operations, making it suitable for various application scenarios. This class inherits from `BaseTurboQuery` and implements the `IQueryExecutor<T>` interface.

## Features

- **Asynchronous and Synchronous Execution**: Provides both `ExecuteReaderAsync` and `ExecuteReader` methods for asynchronous and synchronous query execution, respectively.
- **Parameterized Queries**: Supports parameterized queries to prevent SQL injection.
- **Custom Mapping**: Allows custom mapping of SQL result sets to objects of type `T` using delegate functions.

## Usage

### Asynchronous Execution

To execute a SQL query asynchronously and map the results to a collection of objects, use the `ExecuteReaderAsync` method:

```csharp
public async Task<IEnumerable<T>> ExecuteReaderAsync(string Query, Func<SqlDataReader, T> mapFunction)
```

### Examples
---
1) Asynchronously Fetching Data without query parameters
```csharp
IEnumerable<User> users = await QueryExecutor<User>.ExecuteReaderAsync("Select * from Users;", reader =>
{
    return new User { ID = 0, Username = reader.GetString(reader.GetOrdinal("Username")), };

});
```
2) Synchronously Fetching Data With Query Parameters 
```csharp
IEnumerable<User> users = QueryExecutor<User>.ExecuteReader("Select * from Users where Status = @Status;", cmd =>
            {
                cmd.Parameters.AddWithValue("@Status", 1);
            }, reader =>
            {
                return new User { ID = 0, Username = reader.GetString(reader.GetOrdinal("Username")), };
            });
```

***Not:*** The tow cases in valid for asynchronously and synchronously case.

---

### QueryBatchRecords

#### `QueryBatchRecords<T> Class`

The `QueryBatchRecords<T>` class is a utility designed to retrieve paginated subsets of data from a database table using a stored procedure. It supports both asynchronous and synchronous operations, making it suitable for applications that require efficient data retrieval in batches. This class inherits from `BaseTurboQuery` and implements the `IQueryBatchRecords<T>` interface.

## Features

- **Paginated Data Retrieval**: Retrieves data in paginated form using a stored procedure.
- **Asynchronous and Synchronous Execution**: Provides both `BatchingTableAsync` and `BatchingTable` methods for asynchronous and synchronous paginated data retrieval, respectively.
- **Custom Mapping**: Allows custom mapping of SQL result sets to objects of type `T` using delegate functions.
- **Stored Procedure Support**: Executes a predefined stored procedure (`SP_TablePagination`) to handle pagination logic.

## Usage

### Asynchronous Paginated Data Retrieval

To retrieve a paginated subset of data asynchronously, use the `BatchingTableAsync` method:

```csharp
public async Task<IEnumerable<T>> BatchingTableAsync(string sql, int pageNumber, int pageSize, Func<SqlDataReader, T> reader)
```

### Examples
---

1) Retrieve paginated data asynchronous
```csharp
IEnumerable<User> users = await QueryBatchRecords<User>.BatchingTableAsync("SELECT * FROM vw_Users  ORDER BY ID", 2, 1, reader =>
            {
                return new User { ID = 0, Username = reader.GetString(reader.GetOrdinal("Username")) };
            });
```

2) Retrieve paginated data synchronous
```caharp
 IEnumerable<User> users = (new QueryBatchRecords<User>()).BatchingTable("SELECT * FROM Users ORDER BY ID", 2, 1, reader =>
            {
                return new User { ID = 0, Username = reader.GetString(reader.GetOrdinal("Username")) };
            });
```

---

### QueryOrphanRecord

# `QueryOrphanRecord<T> Class`

The `QueryOrphanRecord<T>` class is a utility designed to retrieve a single record (or "orphan record") from a database based on a provided SQL query. It supports both asynchronous and synchronous operations, making it suitable for applications that require fetching standalone records. This class inherits from `BaseTurboQuery` and implements the `IQueryOrphanRecord<T>` interface.

## Features

- **Single Record Retrieval**: Retrieves a single record from the database based on a query.
- **Asynchronous and Synchronous Execution**: Provides both `GetOrphanRecordAsync` and `GetOrphanRecord` methods for asynchronous and synchronous record retrieval, respectively.
- **Custom Mapping**: Allows custom mapping of SQL result sets to objects of type `T` using delegate functions.
- **Parameterized Queries**: Supports parameterized queries to prevent SQL injection.

## Usage

### Asynchronous Single Record Retrieval

To retrieve a single record asynchronously, use the `GetOrphanRecordAsync` method:

```csharp
public async Task<T> GetOrphanRecordAsync(string query, Action<SqlCommand> setParameters, Func<SqlDataReader, T> mapFunction)
```

### Examples
1) ASynchronous Single Record Retrieval
```csharp
            User user = await OrphanRecord<User>.GetOrphanRecordAsync("SELECT TOP 1 * FROM Users WHERE Username=@Username;", 
            cmd => cmd.Parameters.AddWithValue("@Username", "FerasBarahmeh"), 
            reader => new User { ID = 0, Username = reader.GetString(reader.GetOrdinal("Username")), }
            );
```
2) Synchronous Single Record Retrieval
```csharp
            User user = await OrphanRecord<User>.GetOrphanRecordAsync(
            "SELECT TOP 1 * FROM Users WHERE Username=@Username;", 
            cmd => cmd => cmd.Parameters.AddWithValue("@Username", "FerasBarahmeh"),
            reader => new User { ID = 0, Username = reader.GetString(reader.GetOrdinal("Username")), });
```
---
### QueryScalarExecutor
# `QueryScalarExecutor<T> Class`

The `QueryScalarExecutor<T>` class is a utility designed to execute SQL queries and return the first column of the first row in the result set as a specified type. It supports both asynchronous and synchronous operations, making it suitable for applications that require retrieving single values from a database. This class inherits from `BaseTurboQuery` and implements the `IQueryScalarExecutor<T>` interface.

## Features

- **Scalar Value Retrieval**: Retrieves the first column of the first row in the result set as a specified type.
- **Asynchronous and Synchronous Execution**: Provides both `ExecuteScalarAsync` and `ExecuteScalar` methods for asynchronous and synchronous scalar value retrieval, respectively.
- **Parameterized Queries**: Supports parameterized queries to prevent SQL injection.
- **Flexible Query Execution**: Allows execution of queries with or without parameters.

## Usage

### Asynchronous Scalar Value Retrieval

To execute a SQL query asynchronously and retrieve the scalar value, use the `ExecuteScalarAsync` method:

```csharp
public async Task<T> ExecuteScalarAsync(string Query, Action<SqlCommand> SetParameters)
```

### Examples 

1) Asynchronously Retrieving a Count of Users with a Parameterized Query
```csharp

     int count = await QueryScalarExecutor<int>.ExecuteScalarAsync(
        "SELECT COUNT(*) FROM Users WHERE Status=@Status;",
        cmd =>   cmd.Parameters.AddWithValue("@Status", 1)
        );
```

2) Synchronously Retrieving a Count of Users with a UnParameterized Query
```csharp
    int count = await QueryScalarExecutor<int>.ExecuteScalarAsync("SELECT COUNT(*) FROM Users;");
```
***Not:*** The tow cases in valid for asynchronously and synchronously case.

---

### QuerySterile

# `QuerySterile Class`

The `QuerySterile` class is a utility designed to execute SQL queries that do not return a result set (e.g., `INSERT`, `UPDATE`, `DELETE`) and retrieve the number of rows affected by the operation. It supports both asynchronous and synchronous operations, making it suitable for applications that require executing non-query SQL commands. This class inherits from `BaseTurboQuery` and implements the `IQuerySterile` interface.

## Features

- **Non-Query Execution**: Executes SQL commands that do not return a result set and returns the number of rows affected.
- **Asynchronous and Synchronous Execution**: Provides both `ExecuteNonQueryAsync` and `ExecuteNonQuery` methods for asynchronous and synchronous execution, respectively.
- **Parameterized Queries**: Supports parameterized queries to prevent SQL injection.
- **Batch Execution**: Allows executing a batch of parameterized SQL queries for a collection of objects.

## Usage

### Asynchronous Non-Query Execution

To execute a SQL command asynchronously and retrieve the number of rows affected, use the `ExecuteNonQueryAsync` method:

```csharp
public async Task<int> ExecuteNonQueryAsync(string Query, Action<SqlCommand> SetParams)
```

### Examples

1) ASynchronous Non-Query Execution
```csharp
var rowsAffected = await (new QuerySterile()).ExecuteNonQueryAsync(
    "UPDATE Users SET IsActive = @IsActive WHERE Id = @Id",
    cmd =>
    {
        cmd.Parameters.AddWithValue("@IsActive", false);
        cmd.Parameters.AddWithValue("@Id", 1);
    });
```
2) Asynchronous Non-Query Execution Without Parameters
```csharp
    var rowsAffected = await  (new QuerySterile()).ExecuteNonQueryAsync("DELETE FROM Users WHERE IsActive = 0");
```
3) Asynchronous Batch Non-Query Execution
```csharp
    var users = new List<User>
    {
        new User { Id = 1, Username = "Alice" },
        new User { Id = 2, Username = "Bob" }
    };

    int rowsProcessed = await (new QuerySterile()).ExecuteBatchNonQueryAsync(
        "INSERT INTO Users (Id, Username) VALUES (@Id, @Username);",
        users,
        (user, command) =>
        {
            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@Username", user.Username);
        });
```
***Another Example Delete By ID's***
```csharp
 IEnumerable<int> users = new List<int> { 6 };

            return await (new QuerySterile()).ExecuteBatchNonQueryAsync<int>(
            "DELETE FROM users WHERE ID IN (@ID);", 
            users,
            (user, cmd) => cmd.Parameters.AddWithValue("@Id", user));
```

---

### DataMapper

# `DataMapper Class`

The `DataMapper` class is a static utility designed to simplify the process of mapping data from a `SqlDataReader` to strongly-typed values. It provides a generic method to safely retrieve values from a `SqlDataReader` by column name, handling `DBNull` values gracefully.

## Features

- **Safe Value Retrieval**: Retrieves values from a `SqlDataReader` by column name and handles `DBNull` values by returning the default value for the specified type.
- **Generic Method**: Supports mapping to any type `T`, making it flexible for various data types.
- **Simplified Data Mapping**: Reduces boilerplate code when working with `SqlDataReader` and ensures type safety.

## Usage

### Retrieving Values from SqlDataReader

To retrieve a value from a `SqlDataReader` by column name, use the `GetValue<T>` method:

```csharp
public static T GetValue<T>(SqlDataReader reader, string columnName)
```

### Examples

1) Mapping for integers
```csharp
    int id = DataMapper.GetValue<int>(reader, "Id");
```
2) Mapping for string 
```csharp
    string name = DataMapper.GetValue<string>(reader, "Name");
```
3) mapping for Nullable
```csharp
    int? age = DataMapper.GetValue<int?>(reader, "Age"); 
```

--- 

## More Details

1) [Sql Pagination Query](https://github.com/FerasBarahmeh/TurboQuery/blob/master/Scripts/setup.sql)

---

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature or bugfix.
3. Submit a pull request with a detailed description of your changes.

## License

This project is licensed under the [MIT License](https://github.com/FerasBarahmeh/TurboQuery/blob/master/LICENSE.txt).