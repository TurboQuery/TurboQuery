using Microsoft.Data.SqlClient;

namespace TurboQuery.Helpers;

public static class DataMapper
{
    public static T GetValue<T>(SqlDataReader reader, string columnName)
    {
        int columnIndex = reader.GetOrdinal(columnName);

        if (reader.IsDBNull(columnIndex))
        {
            return default(T);
        }

        return (T)reader.GetValue(columnIndex);
    }
}
