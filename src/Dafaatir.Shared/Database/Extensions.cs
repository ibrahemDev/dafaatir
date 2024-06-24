using System.Reflection;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using Dafaatir.Shared.Env;
using Dafaatir.Shared.Database.Sqlite;


namespace Dafaatir.Shared.Database;
public static class Extensions
{

    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {


        if (EnvManager.GetEnvData().DatabaseDriver == DatabaseDriverEnum.sqlite)
        {
            services.AddSqlite();
        }
        else
        {
            throw new Exception("now only spuurt sqlite");
        }







        return services;
    }
}