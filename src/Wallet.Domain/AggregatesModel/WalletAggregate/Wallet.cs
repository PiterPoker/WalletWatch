namespace Wallet.Domain.AggregatesModel.WalletAggregate
{
    public class Wallet : Entity, IAggregateRoot
    {
        private List<Member> _familyMembers;

        public string Name { get; private set; }
        public Wallet? MainWallet { get; private set; }
        public decimal Balance { get; private set; }
        public long FamilyId { get; private set; }

        private int _currencyId;
        public Currency Currency { get; private set; }

        public IEnumerable<Member> FamilyMembers => _familyMembers.AsReadOnly();
    }
}
