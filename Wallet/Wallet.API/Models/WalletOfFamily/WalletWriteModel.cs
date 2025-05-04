using Wallet.API.Models.Base;

namespace Wallet.API.Models.WalletOfFamily;

public record WalletWriteModel : FinancialWriteModel
{
    public required string Description { get; init; }
    public required FamilyWriteModel Family { get; init; }
}
