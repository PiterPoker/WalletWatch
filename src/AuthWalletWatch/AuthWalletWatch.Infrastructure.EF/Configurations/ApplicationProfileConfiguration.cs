using AuthWalletWatch.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthWalletWatch.Infrastructure.EF.Configurations;

internal class ApplicationProfileConfiguration : IEntityTypeConfiguration<ApplicationProfile>
{
    public void Configure(EntityTypeBuilder<ApplicationProfile> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(p => p.LastName).IsRequired().HasMaxLength(50);
        builder.Property(p => p.DateOfBirth).IsRequired(false);
        builder.Property(p => p.PhotoUrl).IsRequired(false);
        builder.Property(p => p.PhoneNumber).IsRequired(false);

        builder.HasOne(p => p.User)
        .WithOne(u => u.Profile)
        .HasForeignKey<ApplicationProfile>(p => p.UserId);
    }
}
