using Expenses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Drawing;

namespace Expenses.Infrastructure.Configurations;

/// <summary>
/// Configuration for the Category entity for Entity Framework Core.
/// </summary>
internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    /// <summary>
    /// Configures the mapping of the Category entity to the database table.
    /// </summary>
    /// <param name="builder">The builder used to configure the Category entity.</param>
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Specifies the table name to which the Category entity will be mapped.
        builder.ToTable("categories");

        // Configures the Id property (primary key):
        // - Specifies the column name "id".
        // - Uses the HiLo key generation strategy with the sequence name "category_seq".
        builder.Property(c => c.Id)
            .HasColumnName("id");
        //    .UseHiLo("category_seq");

        // Configures the Name property:
        // - Specifies the column name "name".
        // - Specifies that the property is required (NOT NULL).
        // - Sets the maximum string length to 255 characters.
        builder.Property(c => c.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(255);

        // Configures the Color property:
        // - Specifies the column name "color".
        // - Allows null values.
        builder.Property(c => c.Color)
            .HasColumnName("color")
            .IsRequired(false)
            .HasConversion(new ValueConverter<Color?, string>(
                v => v.HasValue ? v.Value.Name : null, // Преобразование Color? в строку
                v => string.IsNullOrEmpty(v) ? (Color?)null : Color.FromName(v) // Преобразование строки в Color?
            ))
            .HasMaxLength(100);

        // Configures the relationship with the Author entity:
        // - Specifies the foreign key property "AuthorId".
        // - Specifies that the relationship is required (NOT NULL).
        builder.HasOne(c => c.Author)
            .WithMany()
            .HasForeignKey("author_id")
            .IsRequired();

        // TODO: Add a unique index for the Name property if necessary.
        // builder.HasIndex(c => c.Name).IsUnique();

        // TODO: Add other necessary settings (data type, constraints, comments).
    }
}