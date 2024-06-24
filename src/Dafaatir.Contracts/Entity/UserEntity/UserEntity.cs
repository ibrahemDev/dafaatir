using Dafaatir.Contracts.Entity;

namespace Dafaatir.Contracts.Entity;

/// <summary>
/// Represents a user entity.
/// </summary>
public class UserEntity : NormalEntity<UserId>, INormalEntity<UserId>
{
    /// <summary>
    /// Gets or sets the display name of the user.
    /// </summary>
    public required string DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user's email address has been verified.
    /// </summary>
    public required bool EmailVerified { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the user was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the roles assigned to the user.
    /// </summary>
    public virtual ICollection<RoleEntity> Roles { get; set; } = null!;
    public virtual ICollection<UserRoleEntity> UserRoles { get; set; } = null!;


    /// <summary>
    /// Gets or sets the access tokens associated with this user.
    /// </summary>
    public virtual ICollection<AccessTokenEntity> AccessTokens { get; set; } = null!;

    /// <summary>
    /// Gets or sets the refresh tokens associated with this user.
    /// </summary>
    public virtual ICollection<RefreshTokenEntity> RefreshTokens { get; set; } = null!;


    /// <summary>
    /// Initializes a new instance of the <see cref="UserEntity"/> class.
    /// </summary>
    /// <param name="id">The user ID.</param>
    /// <param name="displayName">The display name of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <param name="email">The email address of the user.</param>
    /// <param name="emailVerified">A value indicating whether the user's email address has been verified.</param>
    public UserEntity(UserId id) : base(id)
    {

    }



    /// <summary>
    /// Creates a new instance of the <see cref="UserEntity"/> class without an ID for insert.
    /// </summary>
    /// <param name="displayName">The display name of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <param name="email">The email address of the user.</param>
    /// <param name="emailVerified">A value indicating whether the user's email address has been verified.</param>
    /// <returns>A new instance of the <see cref="UserEntity"/> class.</returns>
    public static UserEntity Create(string displayName, string password, string email, bool emailVerified)
    {
        return new UserEntity(UserId.CreateEmpty())
        {
            DisplayName = displayName,
            Email = email,
            Password = password,
            EmailVerified = emailVerified
        };
    }
}
