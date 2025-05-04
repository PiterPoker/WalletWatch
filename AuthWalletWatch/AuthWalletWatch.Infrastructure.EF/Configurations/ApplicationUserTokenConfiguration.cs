using AuthWalletWatch.Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AuthWalletWatch.Infrastructure.EF.Configurations;

internal class ApplicationUserTokenConfiguration : IEntityTypeConfiguration<ApplicationUserToken>
{
    public void Configure(EntityTypeBuilder<ApplicationUserToken> builder)
    {
        /*builder.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
        builder.Property(ut => ut.Value).HasMaxLength(256);*/
        builder.Property(ut => ut.ExpiresAt)
            .HasColumnName("expires_at");

        builder.Property(e => e.Create)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            ).HasColumnName("create");
        /*builder.Property(ut => ut.Create)
            .HasColumnName("create");*/

        builder.HasOne(ut => ut.User)
            .WithMany(u => u.Tokens)
            .IsRequired();
    }
}