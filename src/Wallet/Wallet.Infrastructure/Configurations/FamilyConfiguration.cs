using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Domain.Models.WalletOfFamily;

namespace Wallet.Infrastructure.Configurations;

internal class FamilyConfiguration
    : IEntityTypeConfiguration<Family>
{
    public void Configure(EntityTypeBuilder<Family> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Name)
               .IsRequired()
               .HasMaxLength(255);
        builder.HasOne(f => f.HeadMember)
            .WithMany()
            .HasForeignKey("HeadMemberId");
    }
}
