namespace Wallet.Domain.AggregatesModel.AccountAggregate
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account Add(Account account);

        void Update(Account account);

        Task<Account> GetAsync(int accountId);
        Task DeleteAsync(int accountId);
    }
}
