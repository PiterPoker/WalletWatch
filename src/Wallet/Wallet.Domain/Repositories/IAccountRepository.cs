using Wallet.Domain.SeedWork;
using WalletOfFamily = Wallet.Domain.Models.WalletOfFamily;

namespace Wallet.Domain.Repositories;

public interface IAccountRepository<TEntity, in TId> : IRepository<TEntity, TId>
where TEntity : Entity<TId>
where TId : struct
{
    Task<REntity> GetSameWalletById<REntity>(TId id, CancellationToken cancellation)
        where REntity : WalletOfFamily.Wallet;
}
