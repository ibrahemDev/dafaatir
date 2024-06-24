
using Dafaatir.Contracts.Entity;
using Dafaatir.Contracts.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;



namespace EcommerceStore.Infrastructure.Repository;


public abstract class GenericRepository<T, TId, TDC> : IGenericRepository<T, TId>
    where T : NormalEntity<TId>, INormalEntity<TId>
    where TId : EntityId<TId>, IEntityId<TId>
    where TDC : DbContext
{


    public TDC _context;

    public GenericRepository(TDC context)
    {

        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }


    /************************************************************************************************************/
    /****************************************************GET*****************************************************/
    /************************************************************************************************************/


    public async Task<List<T>> GetAllAsync(Expression<Func<DbSet<T>, DbSet<T>>>? dbSet = null)
    {




        DbSet<T> db = _context.Set<T>();
        db = dbSet?.Compile()(db) ?? db;
        return await db.ToListAsync();
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<DbSet<T>, IQueryable<T>>>? dbSetQueryable = null)
    {
        DbSet<T> db = _context.Set<T>();
        var q = dbSetQueryable?.Compile()(db) ?? db.AsQueryable();
        return await q.ToListAsync();
    }





    public async Task<List<T>> GetAllAsync(Expression<Func<IQueryable<T>, IQueryable<T>>>? queryable = null)
    {




        IQueryable<T> q = _context.Set<T>().AsQueryable();
        q = queryable?.Compile()(q) ?? q;
        return await q.ToListAsync();
    }



    public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<IQueryable<T>, IQueryable<T>>>? queryable = null)
    {

        IQueryable<T> q = _context.Set<T>().AsQueryable();
        q = queryable?.Compile()(q) ?? q;
        return await q.FirstOrDefaultAsync();
    }


    public async Task<T?> GetLastOrDefaultAsync(Expression<Func<IQueryable<T>, IQueryable<T>>>? queryable = null)
    {
        IQueryable<T> q = _context.Set<T>().AsQueryable();
        q = queryable?.Compile()(q) ?? q;
        return await q.LastOrDefaultAsync();
    }



    public async Task<int> GetCountAsync(Expression<Func<IQueryable<T>, IQueryable<T>>>? queryable = null)
    {

        IQueryable<T> q = _context.Set<T>().AsQueryable();
        q = queryable?.Compile()(q) ?? q;
        return await q.CountAsync();

    }


    public async Task<long> GetLongCountAsync(Expression<Func<IQueryable<T>, IQueryable<T>>>? queryable = null)
    {
        IQueryable<T> q = _context.Set<T>().AsQueryable();
        q = queryable?.Compile()(q) ?? q;
        return await q.LongCountAsync();
    }




    /************************************************************************************************************/
    /****************************************************ADD*****************************************************/
    /************************************************************************************************************/


    public async Task<T> AddAsync(T entity)
    {
        DbSet<T> db = _context.Set<T>();
        var q = await db.AddAsync(entity);
        await _context.SaveChangesAsync();
        return q.Entity;
    }



    public async Task<int> DeleteAsync(Expression<Func<IQueryable<T>, IQueryable<T>>>? queryable = null)
    {

        IQueryable<T> q = _context.Set<T>().AsQueryable();
        q = queryable?.Compile()(q) ?? q;
        var users = await q.ToListAsync();
        users.ForEach(a =>
           {
               _context.Remove(a);
           });

        return await _context.SaveChangesAsync();




    }

    public async Task UpdateAsync(T entity)
    {
        DbSet<T> db = _context.Set<T>();
        db.Update(entity);
        await _context.SaveChangesAsync();
    }





    public async Task Edit(T entity)
    {
        var entry = _context.Entry(entity);
        entry.State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }




    public virtual void Detach(T entity)
    {
        _context.Entry(entity).State = EntityState.Detached;
    }



    public virtual void DetachAll(List<T> entities)
    {
        if (entities != null)
        {
            foreach (var changeEntry in entities)
            {
                _context.Entry(changeEntry).State = EntityState.Detached;
            }
        }
    }

    public Task<List<INormalEntity<TId>>> GetAllAsync(Expression<Func<DbSet<INormalEntity<TId>>, DbSet<INormalEntity<TId>>>>? dbSet = null)
    {
        throw new NotImplementedException();
    }

    public Task<List<INormalEntity<TId>>> GetAllAsync(Expression<Func<DbSet<INormalEntity<TId>>, IQueryable<INormalEntity<TId>>>>? dbSetQueryable = null)
    {
        throw new NotImplementedException();
    }
}
