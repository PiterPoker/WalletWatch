using AuthWalletWatch.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthWalletWatch.Infrastructure.EF.Configurations
{
    internal class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            /*builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).HasMaxLength(256);
            builder.Property(r => r.NormalizedName).HasMaxLength(256);*/
            builder.Property(r => r.Description).IsRequired(false);

            builder.HasMany(r => r.Users)
                .WithMany(u => u.Roles)
                .UsingEntity<ApplicationUserRole>(
                    ur => ur.HasOne<ApplicationUser>().WithMany().HasForeignKey(ur => ur.UserId),
                    ur => ur.HasOne<ApplicationRole>().WithMany().HasForeignKey(ur => ur.RoleId));

            builder.HasMany(r => r.RoleClaims)
                .WithOne(r => r.Role)
                .HasForeignKey(r => r.RoleId)
                .IsRequired();
        }
    }
}
