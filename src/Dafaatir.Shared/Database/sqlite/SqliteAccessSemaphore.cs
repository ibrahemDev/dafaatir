using System.Threading;
namespace Dafaatir.Shared.Database.Sqlite;

public class SqliteAccessSemaphore
{
    private static readonly SemaphoreSlim semaphore = new(1, 1);

    private SqliteAccessSemaphore() { }

    public static SemaphoreSlim Instance => semaphore;
}





