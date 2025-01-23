-- Create Pagination Procedure.sql
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_BatchingRecords]') AND type IN (N'P', N'PC'))
BEGIN
    EXEC('
    CREATE PROCEDURE SP_BatchingRecords
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
                   N'' OFFSET '' + CAST((@PageNumber - 1) * @PageSize AS NVARCHAR(10)) + N'' ROWS '' +
                   N'' FETCH NEXT '' + CAST(@PageSize AS NVARCHAR(10)) + N'' ROWS ONLY;'';

        -- Execute the dynamic SQL
        EXEC sp_executesql @SQL;

        -- Get the number of rows returned
        SELECT @@ROWCOUNT AS NumberOfRows;
    END;
    ')
END