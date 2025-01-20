
# Turbo Query

TurboQuery is a .NET library designed to simplify and optimize database query execution. It provides a set of classes and methods to handle query execution, batch processing, and scalar queries efficiently.

## Table of Contents

1. [Installation](#installation)
2. [Usage](#usage)
3. [Classes and Methods Documentation](#classes-and-methods-documentation)
   - [QueryExecutor](#queryexecutor)
   - [QueryBatchRecords](#querybatchrecords)
   - [QueryScalarExecutor](#queryscalarexecutor)
   - [QuerySterile](#querysterile)
   - [ServiceCollectionExtensions](#servicecollectionextensions)
4. [Examples](#examples)
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

## Usage

To use the library, include the namespace in your project:

```csharp
using TurboQuery;
```

Then, create an instance of the desired class or use the provided extension methods.

```csharp
var queryExecutor = new QueryExecutor();
```

---

## Classes and Methods Documentation

Below is the documentation for each class and its methods in the package.

### BaseTurboQuery

#### `BaseTurboQuery`

**Description**:  
This is the base class for all query-related operations. It provides common functionality and properties used by other query classes.

**Properties**:
- `ConnectionString` (string): The database connection string.

**Methods**:
- `Initialize(string connectionString)`: Initializes the query executor with the provided connection string.

**Example**:
```csharp
var baseQuery = new BaseTurboQuery();
baseQuery.Initialize("your_connection_string");
```

---

### QueryExecutor

#### `QueryExecutor`

**Description**:  
This class is responsible for executing SQL queries and returning results.

**Methods**:
- `ExecuteQuery(string sql)`: Executes the provided SQL query and returns the result as a `DataTable`.

**Example**:
```csharp
var queryExecutor = new QueryExecutor();
var result = queryExecutor.ExecuteQuery("SELECT * FROM Users");
```

---

### QueryBatchRecords

#### `QueryBatchRecords`

**Description**:  
This class handles batch processing of records, allowing for efficient bulk operations.

**Methods**:
- `BatchInsert(string tableName, DataTable data)`: Inserts a batch of records into the specified table.

**Example**:
```csharp
var batchRecords = new QueryBatchRecords();
batchRecords.BatchInsert("Users", userDataTable);
```

---

### QueryScalarExecutor

#### `QueryScalarExecutor`

**Description**:  
This class is used for executing scalar queries, which return a single value.

**Methods**:
- `ExecuteScalar(string sql)`: Executes the provided SQL query and returns a single value.

**Example**:
```csharp
var scalarExecutor = new QueryScalarExecutor();
var count = scalarExecutor.ExecuteScalar("SELECT COUNT(*) FROM Users");
```

---

### QuerySterile

#### `QuerySterile`

**Description**:  
This class provides methods for executing queries without returning any results, typically used for insert, update, or delete operations.

**Methods**:
- `ExecuteNonQuery(string sql)`: Executes the provided SQL query without returning any results.

**Example**:
```csharp
var sterileQuery = new QuerySterile();
sterileQuery.ExecuteNonQuery("DELETE FROM Users WHERE Id = 1");
```

---

### ServiceCollectionExtensions

#### `ServiceCollectionExtensions`

**Description**:  
This static class provides extension methods for registering TurboQuery services in the dependency injection container.

**Methods**:
- `AddTurboQuery(this IServiceCollection services, string connectionString)`: Registers TurboQuery services with the provided connection string.

**Example**:
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddTurboQuery("your_connection_string");
}
```

---

## Examples

Here are some complete examples of how to use the package:

### Example 1: Using QueryExecutor
```csharp
var queryExecutor = new QueryExecutor();
var result = queryExecutor.ExecuteQuery("SELECT * FROM Users");
Console.WriteLine(result.Rows.Count);
```

### Example 2: Using QueryBatchRecords
```csharp
var batchRecords = new QueryBatchRecords();
batchRecords.BatchInsert("Users", userDataTable);
```

### Example 3: Using ServiceCollectionExtensions
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddTurboQuery("your_connection_string");
}
```

---

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature or bugfix.
3. Submit a pull request with a detailed description of your changes.

---

## License

This project is licensed under the [MIT License](LICENSE).

---

```
CREATE PROCEDURE SP_TablePagination
    @Query      NVARCHAR(MAX),  -- SQL query without ORDER BY clause
    @PageNumber INT,            -- Page number (1-based)
    @PageSize   INT             -- Number of records per page
AS
BEGIN
    -- Ensure PageNumber is at least 1 and PageSize is greater than 0
    IF @PageNumber < 1 
        SET @PageNumber = 1;
    IF @PageSize < 1 
        SET @PageSize = 10;

    -- Declare the dynamic SQL query
    DECLARE @SQL NVARCHAR(MAX);

    -- Build the dynamic SQL query with OFFSET and FETCH NEXT
    SET @SQL = @Query + 
               N' OFFSET ' + CAST((@PageNumber - 1) * @PageSize AS NVARCHAR(10)) + N' ROWS ' +
               N' FETCH NEXT ' + CAST(@PageSize AS NVARCHAR(10)) + N' ROWS ONLY;';

    -- Execute the dynamic SQL
    EXEC sp_executesql @SQL;

    -- Get the number of rows returned
    SELECT @@ROWCOUNT AS NumberOfRows;
END;
```