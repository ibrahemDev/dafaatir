namespace Dafaatir.Contracts.Entity;

public abstract class NormalEntity<TId>(TId id) : BaseEntity, IBaseEntity, INormalEntity<TId> where TId : IEntityId<TId>
{
    public TId Id { get; set; } = id;
}
