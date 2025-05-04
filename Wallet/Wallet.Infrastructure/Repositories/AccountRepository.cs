using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Repositories;
using Wallet.Domain.SeedWork;
using WalletOfFamily = Wallet.Domain.Models.WalletOfFamily;

namespace Wallet.Infrastructure.Repositories;

public class AccountRepository<TEntity, TId> : Repository<TEntity, TId>, IAccountRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : struct
{
    public AccountRepository(WalletDBContext context) : base(context)
    {
    }

    public async virtual Task<REntity> GetSameWalletById<REntity>(TId id, CancellationToken cancellation)
        where REntity : WalletOfFamily.Wallet
    {
        DbSet<REntity> _dbSet = _context.Set<REntity>();
        return await _dbSet.FirstAsync(entity => entity.Id.Equals(id), cancellation);
    }
}
