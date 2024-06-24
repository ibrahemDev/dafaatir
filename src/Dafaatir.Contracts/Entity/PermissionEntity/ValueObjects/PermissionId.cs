

namespace Dafaatir.Contracts.Entity;

/// <summary>
/// Represents a strongly-typed ID for a permission.
/// </summary>
public class PermissionId : EntityId<PermissionId>, IEntityId<PermissionId>
{

    private PermissionId(object? value) : base(value)
    {

    }

    public static PermissionId Create(object value)
    {
        return new PermissionId(value);
    }

    public static PermissionId CreateEmpty()
    {
        return new PermissionId(null);
    }

    public bool IsEmpty()
    {
        if (Value == null)
        {
            return true;
        }
        return false;
    }
}