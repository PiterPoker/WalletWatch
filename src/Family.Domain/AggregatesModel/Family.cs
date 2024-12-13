namespace Family.Domain.AggregatesModel
{
    public class Family : Entity, IAggregateRoot
    {
        public required string _description;

        // DDD Patterns comment
        // Using a private collection field, better for DDD Aggregate's encapsulation
        // so FamilyMembers cannot be added from "outside the AggregateRoot" directly to the collection,
        // but only through the method FamilyMembersAggregateRoot.AddFamilyMembersItem() which includes behavior.
        private readonly List<Member> _familyMembers;
        public IReadOnlyCollection<Member> FamilyMembers => _familyMembers.AsReadOnly();

        private readonly List<Wallet> _familyWallets;
        public IReadOnlyCollection<Wallet> FamilyWallets => _familyWallets.AsReadOnly();

        public Reports Reports { get; private set; }


        #region Ctors
        protected Family()
        {
            _familyMembers = new List<Member>();
            _familyWallets = new List<Wallet>();
        }

        public Family(string description) : this()
        {
            _description = description;
        }

        #endregion
    }
}
