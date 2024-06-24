using Dafaatir.Contracts.Entity;
using Dafaatir.Shared.Env;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dafaatir.Modules.Users.Infrastructure;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleEntity>
{
    public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
    {
        if (EnvManager.GetEnvData().DatabaseDriver == DatabaseDriverEnum.sqlite)
        {
            SqliteConfigure(builder);
        }
        else if (EnvManager.GetEnvData().DatabaseDriver == DatabaseDriverEnum.postgres)
        {
            PostgresConfigure(builder);
        }
    }

    public void SqliteConfigure(EntityTypeBuilder<UserRoleEntity> builder)
    {
        builder.ToTable("user_roles");


        builder.HasKey(ur => new { ur.UserId, ur.RoleId });
        builder.Property(ur => ur.UserId)
                 .HasColumnName("user_id")
                 .HasColumnType("INTEGER").IsRequired().HasConversion(
                     v => (long)v.Value!,
                     v => UserId.Create(Convert.ToInt64(v)));

        builder.Property(ur => ur.RoleId)
            .HasColumnName("role_id")
            .HasColumnType("INTEGER").IsRequired().HasConversion(
                v => (int)v.Value!,
                v => RoleId.Create(Convert.ToInt32(v)));



        builder.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);




    }

    public void PostgresConfigure(EntityTypeBuilder<UserRoleEntity> builder)
    {
        // Postgres configuration, if needed
    }
}

