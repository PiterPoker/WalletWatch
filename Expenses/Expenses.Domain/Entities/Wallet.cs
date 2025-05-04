using Expenses.Domain.SeedWork;
using System.Diagnostics.CodeAnalysis;

namespace Expenses.Domain.Entities;

/// <summary>
/// Represents a wallet.
/// </summary>
public class Wallet : Entity
{
    /// <summary>
    /// Gets or sets the name of the wallet. This property is required.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Parameterless constructor required by EF Core.  Should not be used directly.
    /// </summary>
    protected Wallet() { }

    /// <summary>
    /// Creates a new Wallet.
    /// </summary>
    /// <param name="name">The name of the wallet.</param>
    [SetsRequiredMembers]
    public Wallet(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name), "Wallet name cannot be null or whitespace.");
        }

        Name = name;
    }

    /// <summary>
    /// Updates the name of the wallet.
    /// </summary>
    /// <param name="name">The new name of the wallet.</param>
    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name), "Wallet name cannot be null or whitespace.");
        }
        Name = name;
    }
}