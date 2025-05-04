using Expenses.Domain.SeedWork;
using System.Diagnostics.CodeAnalysis;

namespace Expenses.Domain.Entities;

/// <summary>
/// Represents an expense transaction.
/// </summary>
public class Expense : Entity
{
    /// <summary>
    /// Gets or private sets the amount of the expense.
    /// </summary>
    public Money Amount { get; private set; }

    /// <summary>
    /// Gets or sets the date and time when the expense occurred.
    /// </summary>
    public DateTime TransactionDate { get; set; }

    /// <summary>
    /// Gets or sets an optional description of the expense.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the author of the expense. This property is required.
    /// </summary>
    public required Author Author { get; set; }

    /// <summary>
    /// Gets or sets the wallet from which the expense was made. This property is required.
    /// </summary>
    public required Wallet Wallet { get; set; }

    /// <summary>
    /// Gets or sets the category of the expense.
    /// </summary>
    public required Category Category { get; set; }


    /// <summary>
    /// Constructor for Expense.  Used by EF Core. Should not be used directly.
    /// </summary>
    protected Expense() 
    { } // EF Core requires a parameterless constructor

    /// <summary>
    /// Creates a new Expense.
    /// </summary>
    /// <param name="amount">The amount of the expense.</param>
    /// <param name="transactionDate">The date and time of the expense.</param>
    /// <param name="author">The author of the expense.</param>
    /// <param name="wallet">The wallet from which the expense was made.</param>
    /// <param name="description">An optional description of the expense.</param>
    [SetsRequiredMembers]
    public Expense(Money amount, DateTime transactionDate, Author? author, Wallet? wallet, Category? category, string? description = null)
    {
        Amount = amount ?? throw new ArgumentNullException(nameof(amount));
        TransactionDate = transactionDate;
        Author = author ?? throw new ArgumentNullException(nameof(author));
        Wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
        Category = category ?? throw new ArgumentNullException(nameof(category));
        Description = description;
    }


    /// <summary>
    /// Updates the expense.
    /// </summary>
    /// <param name="amount">The new amount of the expense.</param>
    /// <param name="transactionDate">The new date and time of the expense.</param>
    /// <param name="description">The new optional description of the expense.</param>
    public void Update(Money amount, DateTime transactionDate, Category? category, string? description = null)
    {
        Amount = amount;
        Category = category ?? throw new ArgumentNullException(nameof(category));
        TransactionDate = transactionDate;
        Description = description;
    }
}