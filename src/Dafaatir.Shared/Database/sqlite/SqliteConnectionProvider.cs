using Microsoft.Data.Sqlite;

namespace Dafaatir.Shared.Database.Sqlite;

public class SqliteConnectionProvider
{
    public SqliteConnection Conn { get; private set; }
    public SqliteConnectionProvider(SqliteConnection _conn)
    {
        Conn = _conn;
        //Conn.OpenAsync();
    }
}
