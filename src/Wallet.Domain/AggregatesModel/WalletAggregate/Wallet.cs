using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Domain.AggregatesModel.WalletAggregate
{
    public class Wallet : Entity, IAggregateRoot
    {
        public required string _name;
        public Wallet? _mainWallet;
        public decimal _balance;
        public long _familyId;

        private int _currencyId;
        public Currency Currency { get; private set; }

        private List<Member> _familyMembers;

        public IEnumerable<Member> FamilyMembers => _familyMembers.AsReadOnly();
    }
}
