namespace Wallet.API.Models.Base;

public abstract record FinancialWriteModel : IWriteModel
{
    public decimal Balance { get; init; }
    public string Currency { get; init; }
}
