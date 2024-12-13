namespace Wallet.Domain.AggregatesModel.WalletAggregate
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        Wallet Add(Wallet wallet);
        void Update(Wallet wallet);
        Task<Wallet> GetAsync(int walletId);
        Task DeleteAsync(int walletId);
    }
}
