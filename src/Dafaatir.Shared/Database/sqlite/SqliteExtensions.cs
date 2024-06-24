using System.Reflection;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Dafaatir.Shared.Database.Sqlite;
using Dafaatir.Shared.Env;


namespace Dafaatir.Shared.Database.Sqlite;
public static class Extensions
{

    public static IServiceCollection AddSqlite(this IServiceCollection services, params Assembly[] assemblies)
    {
        var sqlitePath = EnvManager.GetEnvData().DatabasePath ?? ":memory:";



        if (sqlitePath == ":memory:")
        {
            var conn = new SqliteConnectionProvider(new SqliteConnection($"Filename=${sqlitePath}"));
            // for development only
            services.AddSingleton<SqliteConnectionProvider>(_ => conn);
        }
        else
        {

            string fullPath = Path.IsPathRooted(sqlitePath) ? sqlitePath : Path.GetFullPath(sqlitePath);

            services.AddScoped<SqliteConnectionProvider>(_ => new SqliteConnectionProvider(new SqliteConnection($"Filename=${fullPath}")));
        }


        services.AddDbContext<SqliteDbContext>(dbOptions =>
        {
            string fullPath = "";
            if (sqlitePath != ":memory:")
            {
                fullPath = Path.IsPathRooted(sqlitePath) ? sqlitePath : Path.GetFullPath(sqlitePath);




            }
            else
            {
                fullPath = ":memory:";
            }
            //using SqliteConnection connection = new SqliteConnection($"Filename=${fullPath}");
            //using SqliteConnection connection = new SqliteConnection($"Data Source=${fullPath}");

            //await connection.OpenAsync();
            dbOptions.UseSqlite(sqlitePath
                /*$"Data Source=${fullPath}"*/, b =>
            {
                // b.
                b.MigrationsAssembly("Dafaatir.Migrations");

            });
        });











        return services;
    }
}