namespace Dafaatir.Contracts.Entity;



public class UserRoleEntity : BaseEntity, IBaseEntity
{
    public required UserId UserId { get; set; }

    public required RoleId RoleId { get; set; }

    public virtual RoleEntity Role { get; set; } = null!;
    public virtual UserEntity User { get; set; } = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRoleEntity"/> class.
    /// </summary>
    public UserRoleEntity() : base()
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="UserRoleEntity"/> class.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="roleId">The ID of the role.</param>
    /// <returns>A new instance of the <see cref="UserRoleEntity"/> class.</returns>
    public static UserRoleEntity Create(UserId userId, RoleId roleId)
    {
        return new UserRoleEntity
        {
            UserId = userId,
            RoleId = roleId
        };
    }
}
