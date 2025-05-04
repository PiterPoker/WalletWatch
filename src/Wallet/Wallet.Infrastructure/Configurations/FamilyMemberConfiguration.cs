using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Domain.Models.BaseEntity;
using Wallet.Domain.Models.WalletOfFamily;

namespace Wallet.Infrastructure.Configurations;

internal class FamilyMemberConfiguration
    : IEntityTypeConfiguration<FamilyMember>
{
    public void Configure(EntityTypeBuilder<FamilyMember> builder)
    {
        builder.HasKey(o => o.Id)
            .HasName("OwnerId");
        builder.Property(o => o.Name)
               .IsRequired()
               .HasMaxLength(255);
    }
}
