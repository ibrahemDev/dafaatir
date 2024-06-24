

namespace Dafaatir.Contracts.Entity;

public class PermissionEntity : NormalEntity<PermissionId>, INormalEntity<PermissionId>
{
    /// <summary>
    /// Gets or sets the name of the permission.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the permission.
    /// </summary>
    public string? Description { get; set; } = null;

    /// <summary>
    /// Gets or sets the roles associated with this permission.
    /// </summary>
    public virtual ICollection<RoleEntity>? Roles { get; set; } = null;

    /// <summary>
    /// Gets or sets the role permissions that link this permission to specific roles.
    /// </summary>
    public virtual ICollection<RolePermissionsEntity> RolePermissions { get; set; } = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="PermissionEntity"/> class.
    /// </summary>
    /// <param name="id">The permission ID.</param>
    public PermissionEntity(PermissionId id, string name, string? description = null) : base(id)
    {
        Name = name;
        Description = description;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="PermissionEntity"/> class without an ID for insert.
    /// </summary>
    /// <param name="name">The name of the permission.</param>
    /// <param name="description">The description of the permission.</param>
    /// <returns>A new instance of the <see cref="PermissionEntity"/> class.</returns>
    public static PermissionEntity Create(string name, string? description)
    {
        return new PermissionEntity(PermissionId.CreateEmpty(), name, description);
    }
}