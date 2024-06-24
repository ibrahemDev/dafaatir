namespace Dafaatir.Contracts.Entity;

public abstract class RelationEntity<TId1, TId2>(TId1 id1, TId2 id2) : BaseEntity, IBaseEntity, IRelationEntity<TId1, TId2> where TId1 : IEntityId<TId1> where TId2 : IEntityId<TId2>
{
    public TId1 Id1 { get; set; } = id1;
    public TId2 Id2 { get; set; } = id2;
}
