namespace Wallet.Domain.AggregatesModel.WalletAggregate
{
    public class Currency(int id, string name)
        : Enumeration(id, name)
    {
        public readonly static Currency BYN = new(1, nameof(BYN));
        public readonly static Currency RUB = new(2, nameof(RUB));
        public readonly static Currency USD = new(3, nameof(USD));
    }
}
