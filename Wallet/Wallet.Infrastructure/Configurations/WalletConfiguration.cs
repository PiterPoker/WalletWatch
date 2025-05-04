using Models = Wallet.Domain.Models.WalletOfFamily;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wallet.Infrastructure.Configurations;

internal class WalletConfiguration
    : IEntityTypeConfiguration<Models.Wallet>
{
    public void Configure(EntityTypeBuilder<Models.Wallet> builder)
    {
        builder.HasKey(w => w.Id); // Устанавливаем ключевое поле Id
        builder.Property(w => w.Balance)
            .IsRequired(); // Поле Balance обязательно
        builder.Property(w => w.Currency)
            .IsRequired(); // Поле Currency обязательно
        builder.Property(w => w.Description)
            .HasMaxLength(255); // Максимальная длина строки Description
        builder.HasOne(w => w.Family)
            .WithMany()
            .HasForeignKey("FamilyId");
        builder.HasMany(w => w.SubWallets)
            .WithOne()
            .HasForeignKey("ParentWalletId");
    }
}
