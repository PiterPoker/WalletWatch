using Expenses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expenses.Infrastructure.Configurations;

/// <summary>
/// Configuration for the Expense entity for Entity Framework Core.
/// </summary>
internal class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    /// <summary>
    /// Configures the mapping of the Expense entity to the database table.
    /// </summary>
    /// <param name="builder">The builder used to configure the Expense entity.</param>
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        // Specifies the table name to which the Expense entity will be mapped.
        builder.ToTable("expenses");

        // Configures the Id property (primary key):
        // - Specifies the column name "id".
        // - Uses the HiLo key generation strategy with the sequence name "expense_seq".
        builder.Property(e => e.Id)
            .HasColumnName("id");
        //    .UseHiLo("expense_seq");

        // Configures the TransactionDate property:
        // - Specifies the column name "transaction_date".
        // - Specifies that the property is required (NOT NULL).
        builder.Property(e => e.TransactionDate)
            .HasColumnName("transaction_date")
            .IsRequired();

        // Configures the Description property:
        // - Specifies the column name "description".
        builder.Property(e => e.Description)
            .HasColumnName("description");

        // Configures the relationship with the Author entity:
        // - Specifies the foreign key property "AuthorId".
        // - Specifies that the relationship is required (NOT NULL).
        builder.HasOne(e => e.Author)
            .WithMany()
            .HasForeignKey("author_id")
            .IsRequired();

        // Configures the relationship with the Wallet entity:
        // - Specifies the foreign key property "WalletId".
        // - Specifies that the relationship is required (NOT NULL).
        builder.HasOne(e => e.Wallet)
            .WithMany()
            .HasForeignKey("wallet_id")
            .IsRequired();

        // Configures the relationship with the Category entity:
        // - Specifies the foreign key property "CategoryId".
        // - Specifies that the relationship is required (NOT NULL).
        builder.HasOne(e => e.Category)
            .WithMany()
            .HasForeignKey("category_id")
            .IsRequired();

        // Configures the Money value object:
        builder.OwnsOne(e => e.Amount, money =>
        {
            money.Property(m => m.Value).HasColumnName("amount").IsRequired();
            money.Property(m => m.Currency).HasColumnName("currency").IsRequired();
        });

        // TODO: Add other necessary settings (data type, constraints, comments).
    }
}