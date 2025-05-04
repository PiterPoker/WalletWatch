namespace Wallet.Domain.SeedWork;

public interface IRepository<TEntity, in TId>
    where TEntity : Entity<TId>
    where TId : struct
{
    IQueryable<TEntity> GetAll();
    Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellation);
    Task UpdateAsync(TEntity entity);
    Task AddAsync(TEntity entity, CancellationToken cancellation);
    Task DeleteAsync(TId id, CancellationToken cancellation);
    Task SaveAsync(CancellationToken cancellation);
}