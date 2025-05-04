using Microsoft.EntityFrameworkCore;
using Wallet.Domain.SeedWork;

namespace Wallet.Infrastructure.Repositories;

public class Repository<T, TId> : IRepository<T, TId>
    where T : Entity<TId>
    where TId : struct
{
    protected readonly WalletDBContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(WalletDBContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual IQueryable<T> GetAll()
    {
        return _dbSet.AsQueryable<T>();
    }

    public virtual async Task<T> GetByIdAsync(TId id, CancellationToken cancellation)
    {
        return await _dbSet.FirstAsync(entity => entity.Id.Equals(id), cancellation);
    }

    public virtual async Task AddAsync(T entity, CancellationToken cancellation)
    {
        await _dbSet.AddAsync(entity, cancellation);
    }

    public virtual Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public virtual async Task DeleteAsync(TId id, CancellationToken cancellation)
    {
        var entity = await _dbSet.FindAsync(id, cancellation);
        if (entity is not null)
            _dbSet.Remove(entity);
    }

    public virtual async Task SaveAsync(CancellationToken cancellation)
    {
        await _context.SaveChangesAsync(cancellation);
    }
}
