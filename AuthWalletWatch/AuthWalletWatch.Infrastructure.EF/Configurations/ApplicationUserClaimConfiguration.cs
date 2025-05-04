using AuthWalletWatch.Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AuthWalletWatch.Infrastructure.EF.Configurations;

internal class ApplicationUserClaimConfiguration : IEntityTypeConfiguration<ApplicationUserClaim>
{
    public void Configure(EntityTypeBuilder<ApplicationUserClaim> builder)
    {
        /*builder.HasKey(uc => uc.Id);
        builder.Property(uc => uc.ClaimType).IsRequired().HasMaxLength(256);
        builder.Property(uc => uc.ClaimValue).IsRequired().HasMaxLength(256);*/

        builder.HasOne(uc => uc.User)
            .WithMany(u => u.Claims)
            .IsRequired();
    }
}