using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletOfFamily = Wallet.Domain.Models.WalletOfFamily;

namespace Wallet.Infrastructure.Configurations
{
    internal class SubWalletConfiguration
        : IEntityTypeConfiguration<WalletOfFamily.SubWallet>
    {
        public void Configure(EntityTypeBuilder<WalletOfFamily.SubWallet> builder)
        {
            builder.HasBaseType<WalletOfFamily.Wallet>();
            builder.HasOne(s => s.ParentWallet)
                   .WithMany()
                   .HasForeignKey("ParentWalletId")
                   .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(s => s.FamilyMembers)
                   .WithOne()
                   .HasForeignKey("WalletId");
        }
    }
}
