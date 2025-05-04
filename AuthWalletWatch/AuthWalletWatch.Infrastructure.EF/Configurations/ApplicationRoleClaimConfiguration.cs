using AuthWalletWatch.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthWalletWatch.Infrastructure.EF.Configurations;

internal class ApplicationRoleClaimConfiguration : IEntityTypeConfiguration<ApplicationRoleClaim>
{
    public void Configure(EntityTypeBuilder<ApplicationRoleClaim> builder)
    {
        /*builder.HasKey(rc => rc.Id);
        builder.Property(rc => rc.ClaimType).IsRequired().HasMaxLength(256);
        builder.Property(rc => rc.ClaimValue).IsRequired().HasMaxLength(256);*/

        builder.HasOne(rc => rc.Role)
            .WithMany(r => r.RoleClaims)
            .IsRequired();
    }
}
