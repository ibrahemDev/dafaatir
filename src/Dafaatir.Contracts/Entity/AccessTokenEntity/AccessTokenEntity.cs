using Dafaatir.Contracts.Entity;

namespace Dafaatir.Contracts.Entity;

/// <summary>
/// Represents an access token entity.
/// </summary>
public class AccessTokenEntity : NormalEntity<AccessTokenId>, INormalEntity<AccessTokenId>
{
    /// <summary>
    /// Gets or sets the ID of the user associated with the access token.
    /// </summary>
    public UserId UserId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the refresh token associated with the access token.
    /// </summary>
    public RefreshTokenId RefreshTokenId { get; set; }

    /// <summary>
    /// Gets or sets the secret part of the access token.
    /// </summary>
    public string SecretPart { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the access token was created.
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the access token expires.
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the access token was revoked.
    /// </summary>
    public DateTime? RevokedAt { get; set; }



    public virtual UserEntity User { get; set; } = null!;
    public virtual RefreshTokenEntity RefreshToken { get; set; } = null!;


    /// <summary>
    /// Initializes a new instance of the <see cref="AccessTokenEntity"/> class.
    /// </summary>
    /// <param name="id">The ID of the access token.</param>
    /// <param name="userId">The ID of the user associated with the access token.</param>
    /// <param name="refreshTokenId">The ID of the refresh token associated with the access token.</param>
    /// <param name="secretPart">The secret part of the access token.</param>
    /// <param name="expiresAt">The date and time when the access token expires.</param>
    /// <param name="revokedAt">The date and time when the access token was revoked.</param>
    /// <param name="createdAt">The date and time when the access token was created.</param>
    public AccessTokenEntity(AccessTokenId id, UserId userId, RefreshTokenId refreshTokenId, string secretPart, DateTime expiresAt, DateTime? revokedAt = null, DateTime? createdAt = null) : base(id)
    {
        UserId = userId;
        RefreshTokenId = refreshTokenId;
        SecretPart = secretPart;
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
        RevokedAt = revokedAt;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="AccessTokenEntity"/> class with an empty access token ID.
    /// </summary>
    /// <param name="userId">The ID of the user associated with the access token.</param>
    /// <param name="refreshTokenId">The ID of the refresh token associated with the access token.</param>
    /// <param name="secretPart">The secret part of the access token.</param>
    /// <param name="expiresAt">The date and time when the access token expires.</param>
    /// <param name="revokedAt">The date and time when the access token was revoked.</param>
    /// <param name="createdAt">The date and time when the access token was created.</param>
    /// <returns>A new instance of the <see cref="AccessTokenEntity"/> class.</returns>
    public static AccessTokenEntity Create(UserId userId, RefreshTokenId refreshTokenId, string secretPart, DateTime expiresAt, DateTime? revokedAt = null, DateTime? createdAt = null)
    {
        return new AccessTokenEntity(AccessTokenId.CreateEmpty(), userId, refreshTokenId, secretPart, expiresAt, revokedAt, createdAt);
    }

    /// <summary>
    /// Creates a new instance of an AccessTokenEntity.
    /// </summary>
    /// <param name="accessTokenId">The ID of the access token.</param>
    /// <param name="userId">The ID of the user the access token belongs to.</param>
    /// <param name="refreshTokenId">The ID of the refresh token associated with the access token.</param>
    /// <param name="secretPart">The secret part of the access token.</param>
    /// <param name="expiresAt">The date and time the access token expires.</param>
    /// <param name="revokedAt">The date and time the access token was revoked, if applicable.</param>
    /// <param name="createdAt">The date and time the access token was created.</param>
    /// <returns>A new instance of an AccessTokenEntity.</returns>
    public static AccessTokenEntity Create(AccessTokenId accessTokenId, UserId userId, RefreshTokenId refreshTokenId, string secretPart, DateTime expiresAt, DateTime? revokedAt = null, DateTime? createdAt = null)
    {
        return new AccessTokenEntity(accessTokenId, userId, refreshTokenId, secretPart, expiresAt, revokedAt, createdAt);
    }
}