using Wallet.API.Models.Base;
using Wallet.Domain.Models.WalletOfFamily;

namespace Wallet.API.Models.WalletOfFamily
{
    public record WalletReadModel : FinancialReadModel
    {
        public required string Description { get; init; }
        public required FamilyReadModel Family { get; init; }
        public List<SubWalletReadModel>? SubWallets { get; init; }
    }
}
