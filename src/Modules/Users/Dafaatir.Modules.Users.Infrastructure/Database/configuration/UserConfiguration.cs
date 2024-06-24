



using System.ComponentModel.DataAnnotations.Schema;
using Dafaatir.Contracts.Entity;
using Dafaatir.Shared.Env;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceStore.Infrastructure.Persistence.Database.Configurations;

public class CategoryConfigurations : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
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

    public void SqliteConfigure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("users");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnType("INTEGER").HasColumnName("user_id").ValueGeneratedOnAdd().HasConversion(
                v => (long)v.Value!,
                v => UserId.Create(Convert.ToInt64(v)));

        builder.Property(u => u.DisplayName)
            .HasColumnName("display_name")
            .IsRequired();

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .IsRequired();

        builder.Property(u => u.Password)
            .HasColumnName("password")
            .IsRequired();

        builder.Property(u => u.EmailVerified)
            .HasColumnName("email_verified")
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();



        builder.HasIndex(u => u.Id).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();

        // Relationship with UserRoleEntity (Many-to-Many through UserRoleEntity)
        builder
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<UserRoleEntity>(
                l => l.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId),
                r => r.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId)
            );





    }

    public void PostgresConfigure(EntityTypeBuilder<UserEntity> builder)
    {

    }


}






