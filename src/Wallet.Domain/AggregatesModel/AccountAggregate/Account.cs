namespace Wallet.Domain.AggregatesModel.AccountAggregate
{
    public class Account : Entity, IAggregateRoot
    {
        private int _currencyId;

        public decimal Balance { get; private set; }
        public long ProfileId { get; private set; }
        public Currency Currency { get; private set; }
    }
}
