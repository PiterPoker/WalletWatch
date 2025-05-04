using AuthWalletWatch.Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AuthWalletWatch.Infrastructure.EF.Configurations;

internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        /*builder.HasKey(u => u.Id);
        builder.Property(u => u.UserName).HasMaxLength(256);
        builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
        builder.Property(u => u.Email).HasMaxLength(256);
        builder.Property(u => u.NormalizedEmail).HasMaxLength(256);
        builder.Property(u => u.PasswordHash).IsRequired(false);
        builder.Property(u => u.SecurityStamp).IsRequired(false);
        builder.Property(u => u.ConcurrencyStamp).IsRequired(false);
        builder.Property(u => u.PhoneNumber).IsRequired(false);
        builder.Property(u => u.EmailConfirmed).IsRequired();
        builder.Property(u => u.PhoneNumberConfirmed).IsRequired();
        builder.Property(u => u.TwoFactorEnabled).IsRequired();
        builder.Property(u => u.LockoutEnd).IsRequired(false);
        builder.Property(u => u.LockoutEnabled).IsRequired();
        builder.Property(u => u.AccessFailedCount).IsRequired();*/

        builder.HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<ApplicationProfile>(p => p.UserId);

        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<ApplicationUserRole>(
                ur => ur.HasOne<ApplicationRole>().WithMany().HasForeignKey(ur => ur.RoleId),
                ur => ur.HasOne<ApplicationUser>().WithMany().HasForeignKey(ur => ur.UserId));

        builder.HasMany(u => u.Claims)
            .WithOne(c => c.User)
            .HasForeignKey(f => f.UserId);

        builder.HasMany(u => u.Logins)
            .WithOne(c => c.User)
            .HasForeignKey(f => f.UserId);

        builder.HasMany(u => u.Tokens)
            .WithOne(c => c.User)
            .HasForeignKey(f => f.UserId);
    }
}