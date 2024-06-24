



namespace Dafaatir.Contracts.Entity;



public class RoleEntity : NormalEntity<RoleId>, INormalEntity<RoleId>
{
    /// <summary>
    /// Gets or sets the name of the role.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the role.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the users assigned to this role. (Read-only)
    /// </summary>
    public virtual ICollection<UserEntity> Users { get; set; } = null!;
    public virtual ICollection<UserRoleEntity> UserRoles { get; set; } = null!;



    /// <summary>
    /// Gets or sets the permissions assigned to this role.
    /// </summary>
    public virtual ICollection<PermissionEntity> Permissions { get; set; } = null!;

    /// <summary>
    /// Gets or sets the role permissions associated with this role.
    /// </summary>
    public virtual ICollection<RolePermissionsEntity> RolePermissions { get; set; } = null!;


    /// <summary>
    /// Initializes a new instance of the <see cref="RoleEntity"/> class.
    /// </summary>
    /// <param name="id">The role ID.</param>
    public RoleEntity(RoleId id) : base(id)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="RoleEntity"/> class without an ID for insert.
    /// </summary>
    /// <param name="name">The name of the role.</param>
    /// <param name="description">The description of the role.</param>
    /// <returns>A new instance of the <see cref="RoleEntity"/> class.</returns>
    public static RoleEntity Create(string name, string description)
    {
        return new RoleEntity(RoleId.CreateEmpty())
        {
            Name = name,
            Description = description
        };
    }
}

