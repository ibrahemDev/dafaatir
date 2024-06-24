namespace Dafaatir.Contracts.Entity;




public class RoleId : EntityId<RoleId>, IEntityId<RoleId>
{

    private RoleId(object? value) : base(value)
    {

    }

    public static RoleId Create(object value)
    {
        return new RoleId(value);
    }

    public static RoleId CreateEmpty()
    {
        return new RoleId(null);
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



