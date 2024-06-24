namespace Dafaatir.Contracts.Entity;

public interface INormalEntity<TId> where TId : IEntityId<TId>
{
    public TId Id { get; }
}