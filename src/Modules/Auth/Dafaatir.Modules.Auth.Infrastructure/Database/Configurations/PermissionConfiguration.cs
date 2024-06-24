using Dafaatir.Contracts.Entity;
using Dafaatir.Shared.Env;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dafaatir.Modules.Users.Infrastructure;

public class PermissionConfiguration : IEntityTypeConfiguration<PermissionEntity>
{
    public void Configure(EntityTypeBuilder<PermissionEntity> builder)
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

    public void SqliteConfigure(EntityTypeBuilder<PermissionEntity> builder)
    {
        builder.ToTable("permissions");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnType("INTEGER").HasColumnName("permission_id").ValueGeneratedOnAdd().HasConversion(
                v => (int)v.Value!,
                v => PermissionId.Create(Convert.ToInt32(v)));

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(p => p.Description)
            .HasColumnName("description");

        builder.HasIndex(p => p.Name).IsUnique();


        builder
           .HasMany(r => r.Roles)
           .WithMany(p => p.Permissions)
           .UsingEntity<RolePermissionsEntity>(
               l => l.HasOne(rp => rp.Role).WithMany(r => r.RolePermissions).HasForeignKey(ur => ur.RoleId),
               r => r.HasOne(rp => rp.Permission).WithMany(p => p.RolePermissions).HasForeignKey(rp => rp.PermissionId)
            );

    }

    public void PostgresConfigure(EntityTypeBuilder<PermissionEntity> builder)
    {
        // Configure for PostgreSQL if needed
    }
}
