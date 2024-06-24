namespace Dafaatir.Contracts.Entity;


public abstract class EntityId<TID> where TID : IEntityId<TID>
{
    public object? Value { get; protected set; } = null;

    protected EntityId(object? value)
    {
        //ArgumentNullException.ThrowIfNull(value);
        Value = value;
    }

    public T getId<T>()
    {
        return (T)Value!;
    }


}
