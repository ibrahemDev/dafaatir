namespace Dafaatir.Contracts.Entity;




public class UserId : EntityId<UserId>, IEntityId<UserId>
{

    private UserId(object? value) : base(value)
    {

    }

    public static UserId Create(object value)
    {
        return new UserId(value);
    }

    public static UserId CreateEmpty()
    {
        return new UserId(null);
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



