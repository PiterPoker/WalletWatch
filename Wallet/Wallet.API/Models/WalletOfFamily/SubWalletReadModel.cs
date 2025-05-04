namespace Wallet.API.Models.WalletOfFamily
{
    public record SubWalletReadModel : WalletReadModel
    {
        public required WalletReadModel ParentWallet { get; init; }
        public List<FamilyMemberReadModel>? FamilyMembers { get; init; }
    }
}
