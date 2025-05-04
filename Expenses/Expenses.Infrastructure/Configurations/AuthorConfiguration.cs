using Expenses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expenses.Infrastructure.Configurations;

/// <summary>
/// Configuration for the Author entity for Entity Framework Core.
/// </summary>
internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    /// <summary>
    /// Configures the mapping of the Author entity to the database table.
    /// </summary>
    /// <param name="builder">The builder used to configure the Author entity.</param>
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        // Specifies the table name to which the Author entity will be mapped.
        builder.ToTable("authors");

        // Configures the Id property (primary key):
        // - Specifies the column name "id".
        // - Uses the HiLo key generation strategy with the sequence name "author_seq".
        builder.Property(o => o.Id)
            .HasColumnName("id");
        //    .UseHiLo("author_seq");

        // Configures the Name property:
        // - Specifies the column name "name".
        // - Specifies that the property is required (NOT NULL).
        // - Sets the maximum string length to 255 characters.
        builder.Property(a => a.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(255);

        // TODO: Add a unique index for the Name property if necessary.
        // builder.HasIndex(a => a.Name).IsUnique();

        // TODO: Add other necessary settings (data type, constraints, comments).
    }
}