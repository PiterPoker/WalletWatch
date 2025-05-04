using AuthWalletWatch.Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AuthWalletWatch.Infrastructure.EF.Configurations;

internal class ApplicationUserLoginConfiguration : IEntityTypeConfiguration<ApplicationUserLogin>
{
    public void Configure(EntityTypeBuilder<ApplicationUserLogin> builder)
    {
        /*builder.HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
        builder.Property(ul => ul.ProviderDisplayName).HasMaxLength(256);*/

        builder.HasOne(ul => ul.User)
            .WithMany(u => u.Logins)
            .IsRequired();
    }
}