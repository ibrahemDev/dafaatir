namespace Dafaatir.Contracts.Entity;
public interface IEntityId<TID> where TID : IEntityId<TID>
{
    abstract static TID CreateEmpty();
    public T getId<T>();

    bool IsEmpty();  // Checks if the ID is empty
}

