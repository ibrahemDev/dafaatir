namespace Dafaatir.Contracts.Entity;




public class AccessTokenId : EntityId<AccessTokenId>, IEntityId<AccessTokenId>
{

    private AccessTokenId(object? value) : base(value)
    {

    }

    public static AccessTokenId Create(object value)
    {
        return new AccessTokenId(value);
    }

    public static AccessTokenId CreateEmpty()
    {
        return new AccessTokenId(null);
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



