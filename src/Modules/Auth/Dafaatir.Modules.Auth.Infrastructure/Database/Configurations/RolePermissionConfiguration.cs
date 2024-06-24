using Dafaatir.Contracts.Entity;
using Dafaatir.Shared.Env;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dafaatir.Modules.Users.Infrastructure;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionsEntity>
{
    public void Configure(EntityTypeBuilder<RolePermissionsEntity> builder)
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

    public void SqliteConfigure(EntityTypeBuilder<RolePermissionsEntity> builder)
    {
        builder.ToTable("role_permissions");

        builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });

        builder.Property(rp => rp.RoleId)
            .HasColumnName("role_id")
            .HasColumnType("INTEGER").IsRequired().HasConversion(
                v => (int)v.Value!,
                v => RoleId.Create(Convert.ToInt32(v)));

        builder.Property(rp => rp.PermissionId)
            .HasColumnName("permission_id")
            .HasColumnType("INTEGER").IsRequired().HasConversion(
                v => (int)v.Value!,
                v => PermissionId.Create(Convert.ToInt32(v)));

        builder.HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(rp => rp.Permission)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(rp => rp.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public void PostgresConfigure(EntityTypeBuilder<RolePermissionsEntity> builder)
    {
        // Postgres configuration, if needed
    }
}
