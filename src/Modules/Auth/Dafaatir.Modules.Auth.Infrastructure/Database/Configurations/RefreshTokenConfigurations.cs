using Dafaatir.Contracts.Entity;
using Dafaatir.Shared.Env;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dafaatir.Modules.Auth.Infrastructure;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
{
    public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
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

    public void SqliteConfigure(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        builder.ToTable("refresh_tokens");
        builder.HasKey(rt => rt.Id);
        builder.Property(rt => rt.Id).HasColumnType("INTEGER").HasColumnName("refresh_token_id").ValueGeneratedOnAdd().HasConversion(
            v => (long)v.Value!,
            v => RefreshTokenId.Create(Convert.ToInt64(v)));

        builder.Property(rt => rt.UserId)
            .HasColumnName("user_id")
            .IsRequired()
            .HasConversion(
                v => (long)v.Value!,
                v => UserId.Create(Convert.ToInt64(v)));

        builder.Property(rt => rt.SecretPart)
            .HasColumnName("secret_part")
            .IsRequired();

        builder.Property(rt => rt.Data)
            .HasColumnName("data")
            .IsRequired();

        builder.Property(rt => rt.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("TEXT");

        builder.Property(rt => rt.ExpiresAt)
            .HasColumnName("expires_at")
            .HasColumnType("TEXT")
            .IsRequired();

        builder.Property(rt => rt.RevokedAt)
            .HasColumnName("revoked_at")
            .HasColumnType("TEXT");

        builder.HasIndex(rt => rt.SecretPart).IsUnique();

        builder
            .HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId);
    }

    public void PostgresConfigure(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        // Configure for PostgreSQL if needed
    }
}
