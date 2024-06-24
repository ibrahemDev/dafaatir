



using System.Reflection;
using Dafaatir.Contracts.Entity;
using Microsoft.EntityFrameworkCore;

namespace Dafaatir.Shared.Database.Sqlite;
public interface ISqliteDbContext { }
public class SqliteDbContext(DbContextOptions<SqliteDbContext> options) : DbContext(options), ISqliteDbContext
{


    private readonly Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserRoleEntity> UserRoles { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);


        foreach (var assembly in assemblies)
        {
            /* modelBuilder.ApplyConfigurationsFromAssembly(assembly, type =>
            {

                //SqliteDbContext
                return type.IsInstanceOfType(typeof(ISqliteDbContext));
                //type.IsSubclassOf(typeof(SqliteDbContext));
            });*/
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
        //modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqliteDbContext).Assembly);


    }

}