namespace Dafaatir.Contracts.Entity;




public class RefreshTokenId : EntityId<RefreshTokenId>, IEntityId<RefreshTokenId>
{

    private RefreshTokenId(object? value) : base(value)
    {

    }

    public static RefreshTokenId Create(object value)
    {
        return new RefreshTokenId(value);
    }

    public static RefreshTokenId CreateEmpty()
    {
        return new RefreshTokenId(null);
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



