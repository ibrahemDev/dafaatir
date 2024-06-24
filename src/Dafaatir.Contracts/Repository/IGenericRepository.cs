using System.Linq.Expressions;
using Dafaatir.Contracts.Entity;
using Microsoft.EntityFrameworkCore;

namespace Dafaatir.Contracts.Repository;




public interface IGenericRepository<T, TId>
    where T : INormalEntity<TId>
    where TId : IEntityId<TId>
{

    public Task<List<INormalEntity<TId>>> GetAllAsync(Expression<Func<DbSet<INormalEntity<TId>>, DbSet<INormalEntity<TId>>>>? dbSet = null);
    public Task<List<INormalEntity<TId>>> GetAllAsync(Expression<Func<DbSet<INormalEntity<TId>>, IQueryable<INormalEntity<TId>>>>? dbSetQueryable = null);

    public Task<List<T>> GetAllAsync(Expression<Func<IQueryable<T>, IQueryable<T>>>? queryable = null);



    public Task<T?> GetFirstOrDefaultAsync(Expression<Func<IQueryable<T>, IQueryable<T>>>? queryable = null);

    public Task<T?> GetLastOrDefaultAsync(Expression<Func<IQueryable<T>, IQueryable<T>>>? queryable = null);

    public Task<int> GetCountAsync(Expression<Func<IQueryable<T>, IQueryable<T>>>? queryable = null);


    public Task<long> GetLongCountAsync(Expression<Func<IQueryable<T>, IQueryable<T>>>? queryable = null);


    public Task<T> AddAsync(T entity);



    public Task<int> DeleteAsync(Expression<Func<IQueryable<T>, IQueryable<T>>>? queryable = null);

    public Task UpdateAsync(T entity);

    public Task Edit(T entity);

    public void Detach(T entity);

    public void DetachAll(List<T> entities);
}