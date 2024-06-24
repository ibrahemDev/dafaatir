using Dafaatir.Contracts.Entity;

namespace Dafaatir.Contracts.Entity;


/// <summary>
/// Represents an access token entity.
/// </summary>
public class RefreshTokenEntity : NormalEntity<RefreshTokenId>, INormalEntity<RefreshTokenId>
{

    /// <summary>
    /// Gets or sets the ID of the user associated with the access token.
    /// </summary>
    public UserId UserId { get; set; }

    /// <summary>
    /// Gets or sets the secret part of the access token.
    /// </summary>
    public string SecretPart { get; set; }


    /// <summary>
    /// Gets or sets the data associated with the access token, used for loading user data on the backend server.
    /// </summary>
    public string Data { get; set; }

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

    /// <summary>
    /// The User that owns this refresh token.
    /// TODO: Consider making this non-nullable to enforce a relationship. This may require database schema changes or migration logic.
    /// </summary>
    public virtual UserEntity User { get; set; } = null!;
    public virtual ICollection<AccessTokenEntity> AccessTokens { get; set; } = null!;

    public virtual AccessTokenEntity CurrentAccessTokenEntity { get; set; } = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshTokenEntity"/> class.
    /// </summary>
    /// <param name="id">The ID of the access token.</param>
    /// <param name="userId">The ID of the user associated with the access token.</param>
    /// <param name="secretPart">The secret part of the access token.</param>
    /// <param name="data">The data associated with the access token.</param>
    /// <param name="expiresAt">The date and time when the access token expires.</param>
    /// <param name="revokedAt">The date and time when the access token was revoked.</param>
    /// <param name="createdAt">The date and time when the access token was created.</param>
    private RefreshTokenEntity(RefreshTokenId id, UserId userId, string secretPart, string data, DateTime expiresAt, DateTime? revokedAt = null, DateTime? createdAt = null) : base(id)
    {
        UserId = userId;
        SecretPart = secretPart;
        Data = data;
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
        RevokedAt = revokedAt;
    }


    /// <summary>
    /// Creates a new instance of the <see cref="RefreshTokenEntity"/> class with an empty access token ID.
    /// </summary>
    /// <param name="userId">The ID of the user associated with the access token.</param>
    /// <param name="secretPart">The secret part of the access token.</param>
    /// <param name="data">The data associated with the access token.</param>
    /// <param name="expiresAt">The date and time when the access token expires.</param>
    /// <param name="revokedAt">The date and time when the access token was revoked.</param>
    /// <param name="createdAt">The date and time when the access token was created.</param>
    /// <returns>A new instance of the <see cref="RefreshTokenEntity"/> class.</returns>
    public static RefreshTokenEntity Create(UserId userId, string secretPart, string data, DateTime expiresAt, DateTime? revokedAt = null, DateTime? createdAt = null)
    {
        return new RefreshTokenEntity(RefreshTokenId.CreateEmpty(), userId, secretPart, data, expiresAt, revokedAt, createdAt);
    }


    /// <summary>
    /// Creates a new instance of the <see cref="RefreshTokenEntity"/> class.
    /// </summary>
    /// <param name="RefreshTokenId">The ID of the access token.</param>
    /// <param name="userId">The ID of the user the access token belongs to.</param>
    /// <param name="secretPart">The secret part of the access token.</param>
    /// <param name="data">Additional data associated with the access token.</param>
    /// <param name="expiresAt">The expiration date and time of the access token.</param>
    /// <param name="revokedAt">The date and time the access token was revoked, if applicable.</param>
    /// <param name="createdAt">The date and time the access token was created.</param>
    /// <returns>A new instance of the <see cref="RefreshTokenEntity"/> class.</returns>
    public static RefreshTokenEntity Create(RefreshTokenId RefreshTokenId, UserId userId, string secretPart, string data, DateTime expiresAt, DateTime? revokedAt = null, DateTime? createdAt = null)
    {
        return new RefreshTokenEntity(RefreshTokenId, userId, secretPart, data, expiresAt, revokedAt, createdAt);
    }
}