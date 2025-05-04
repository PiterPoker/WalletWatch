using Wallet.Domain.SeedWork;
using WalletOfFamily = Wallet.Domain.Models.WalletOfFamily;

namespace Wallet.Infrastructure.Repositories
{
    public class WalletRepository<T, TId> : Repository<T, TId>
        where T : Entity<TId>
        where TId : struct
    {
        public WalletRepository(WalletDBContext context) : base(context)
        {
        }

        public override IQueryable<T> GetAll()
        {
            return base.GetAll().Where(p => !(p is WalletOfFamily.SubWallet) && p is WalletOfFamily.Wallet);
        }
    }
}
