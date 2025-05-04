using Expenses.Domain.SeedWork;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Expenses.Domain.Entities;

/// <summary>
/// Represents a category for expenses.
/// </summary>
public class Category : Entity
{
    /// <summary>
    /// Gets or sets the name of the category. This property is required.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the author of the expense. This property is required.
    /// </summary>
    public required Author Author { get; set; }

    /// <summary>
    /// Gets or sets the color associated with the category. This property is optional.
    /// </summary>
    public Color? Color { get; set; }

    /// <summary>
    /// Parameterless constructor required by EF Core. Should not be used directly.
    /// </summary>
    protected Category() { }

    /// <summary>
    /// Creates a new Category.
    /// </summary>
    /// <param name="name">The name of the category.</param>
    /// <param name="color">The optional color of the category.</param>
    [SetsRequiredMembers]
    public Category(string? name, Author? author, Color? color = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name), "Category name cannot be null or whitespace.");
        Color = color;
        Author = author ?? throw new ArgumentNullException(nameof(author));
    }

    /// <summary>
    /// Updates the category's name and color.
    /// </summary>
    /// <param name="name">The new name of the category.</param>
    /// <param name="color">The new optional color of the category.</param>
    public void Update(string name, Color? color = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name), "Category name cannot be null or whitespace.");
        }

        Name = name;
        Color = color;
    }
}