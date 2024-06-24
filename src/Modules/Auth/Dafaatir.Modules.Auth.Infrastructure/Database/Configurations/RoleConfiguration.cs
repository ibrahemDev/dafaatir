using Dafaatir.Contracts.Entity;
using Dafaatir.Shared.Env;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dafaatir.Modules.Users.Infrastructure;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
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

    public void SqliteConfigure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.ToTable("roles");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnType("INTEGER").HasColumnName("role_id").ValueGeneratedOnAdd().HasConversion(
                v => (int)v.Value!,
                v => RoleId.Create(Convert.ToInt32(v)));



        builder.Property(r => r.Name)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(r => r.Description)
            .HasColumnName("description");

        builder.HasIndex(r => r.Name).IsUnique();

        builder
           .HasMany(r => r.Users)
           .WithMany(u => u.Roles)
           .UsingEntity<UserRoleEntity>(
               l => l.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId),
               r => r.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId)
            );

        builder
           .HasMany(r => r.Permissions)
           .WithMany(p => p.Roles)
           .UsingEntity<RolePermissionsEntity>(
               l => l.HasOne(rp => rp.Permission).WithMany(p => p.RolePermissions).HasForeignKey(rp => rp.PermissionId),
               r => r.HasOne(rp => rp.Role).WithMany(r => r.RolePermissions).HasForeignKey(ur => ur.RoleId)
            );

    }

    public void PostgresConfigure(EntityTypeBuilder<RoleEntity> builder)
    {
        // Configure for PostgreSQL if needed
    }
}

