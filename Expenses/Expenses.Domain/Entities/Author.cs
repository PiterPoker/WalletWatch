using Expenses.Domain.SeedWork;
using System.Diagnostics.CodeAnalysis;

namespace Expenses.Domain.Entities;

/// <summary>
/// Represents the author of an expense.
/// </summary>
public class Author : Entity
{
    /// <summary>
    /// Gets or sets the name of the author. This property is required.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Parameterless constructor required by EF Core. Should not be used directly.
    /// </summary>
    protected Author() { }

    /// <summary>
    /// Creates a new Author.
    /// </summary>
    /// <param name="name">The name of the author.</param>
    [SetsRequiredMembers]
    public Author(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name), "Author name cannot be null or whitespace.");
        }

        Name = name;
    }

    /// <summary>
    /// Updates the author's name.
    /// </summary>
    /// <param name="name">The new name of the author.</param>
    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name), "Author name cannot be null or whitespace.");
        }
        Name = name;
    }
}