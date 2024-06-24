namespace Dafaatir.Contracts.Entity;

public class RolePermissionsEntity : BaseEntity, IBaseEntity
{
    public RoleId RoleId { get; set; }

    public PermissionId PermissionId { get; set; }

    public virtual RoleEntity? Role { get; set; }
    public virtual PermissionEntity? Permission { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RolePermissionsEntity"/> class.
    /// </summary>
    protected RolePermissionsEntity(RoleId roleId, PermissionId permissionId) : base()
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="RolePermissionsEntity"/> class.
    /// </summary>
    /// <param name="roleId">The ID of the role.</param>
    /// <param name="permissionId">The ID of the permission.</param>
    /// <returns>A new instance of the <see cref="RolePermissionsEntity"/> class.</returns>
    public static RolePermissionsEntity Create(RoleId roleId, PermissionId permissionId)
    {
        return new RolePermissionsEntity(roleId, permissionId);
    }
}