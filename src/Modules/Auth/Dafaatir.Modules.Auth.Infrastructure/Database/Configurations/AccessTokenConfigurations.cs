using Dafaatir.Contracts.Entity;
using Dafaatir.Shared.Env;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dafaatir.Modules.Auth.Infrastructure;


public class AccessTokenConfiguration : IEntityTypeConfiguration<AccessTokenEntity>
{
    public void Configure(EntityTypeBuilder<AccessTokenEntity> builder)
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

    public void SqliteConfigure(EntityTypeBuilder<AccessTokenEntity> builder)
    {
        builder.ToTable("access_tokens");
        builder.HasKey(at => at.Id);
        builder.Property(at => at.Id).HasColumnType("INTEGER").HasColumnName("access_token_id").ValueGeneratedOnAdd().HasConversion(
            v => (long)v.Value!,
            v => AccessTokenId.Create(Convert.ToInt64(v)));

        builder.Property(at => at.UserId)
            .HasColumnName("user_id")
            .IsRequired()
            .HasConversion(
                v => (long)v.Value!,
                v => UserId.Create(Convert.ToInt64(v)));

        builder.Property(at => at.RefreshTokenId)
            .HasColumnName("refresh_token_id")
            .IsRequired()
            .HasConversion(
                v => (long)v.Value!,
                v => RefreshTokenId.Create(Convert.ToInt64(v)));

        builder.Property(at => at.SecretPart)
            .HasColumnName("secret_part")
            .IsRequired();

        builder.Property(at => at.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("TEXT");

        builder.Property(at => at.ExpiresAt)
            .HasColumnName("expires_at")
            .HasColumnType("TEXT")
            .IsRequired();

        builder.Property(at => at.RevokedAt)
            .HasColumnName("revoked_at")
            .HasColumnType("TEXT");

        builder.HasIndex(at => at.SecretPart).IsUnique();

        builder
            .HasOne(at => at.User)
            .WithMany(u => u.AccessTokens)
            .HasForeignKey(at => at.UserId);

        builder
            .HasOne(at => at.RefreshToken)
            .WithMany(rt => rt.AccessTokens)
            .HasForeignKey(at => at.RefreshTokenId);
    }

    public void PostgresConfigure(EntityTypeBuilder<AccessTokenEntity> builder)
    {
        // Configure for PostgreSQL if needed
    }
}