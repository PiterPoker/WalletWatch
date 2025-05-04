using Expenses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expenses.Infrastructure.Configurations;

/// <summary>
/// Configuration for the Wallet entity for Entity Framework Core.
/// </summary>
internal class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    /// <summary>
    /// Configures the mapping of the Wallet entity to the database table.
    /// </summary>
    /// <param name="builder">The builder used to configure the Wallet entity.</param>
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        // Specifies the table name to which the Wallet entity will be mapped.
        builder.ToTable("wallets");

        // Configures the Id property (primary key):
        // - Specifies the column name "id".
        // - Uses the HiLo key generation strategy with the sequence name "wallet_seq".
        builder.Property(w => w.Id)
            .HasColumnName("id");
        //    .UseHiLo("wallet_seq");

        // Configures the Name property:
        // - Specifies the column name "name".
        // - Specifies that the property is required (NOT NULL).
        // - Sets the maximum string length to 255 characters.
        builder.Property(w => w.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(255);

        // TODO: Add a unique index for the Name property if necessary.
        // builder.HasIndex(w => w.Name).IsUnique();

        // TODO: Add other necessary settings (data type, constraints, comments).
    }
}