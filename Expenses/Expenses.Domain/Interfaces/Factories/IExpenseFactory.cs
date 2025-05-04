using Expenses.Domain.Entities;

namespace Expenses.Domain.Interfaces.Factories;

/// <summary>
/// Defines an interface for creating instances of the <see cref="Expense"/> entity.
/// </summary>
public interface IExpenseFactory
{
    /// <summary>
    /// Creates a new <see cref="Expense"/> instance asynchronously.
    /// </summary>
    /// <param name="categoryId">The ID of the <see cref="Category"/> to associate with the expense.</param>
    /// <param name="amount">The numerical amount of the expense.</param>
    /// <param name="currency">The currency code of the expense.</param>
    /// <param name="transactionDate">The date and time of the expense transaction.</param>
    /// <param name="walletId">The ID of the <see cref="Wallet"/> from which the expense was made.</param>
    /// <param name="authorId">The ID of the <see cref="Author"/> who created the expense.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result is the created <see cref="Expense"/> entity.</returns>
    Task<Expense> CreateExpense(Guid categoryId, decimal amount, string currency, DateTime transactionDate, Guid walletId, Guid authorId);

    /// <summary>
    /// Creates a new <see cref="Expense"/> instance asynchronously with a description.
    /// </summary>
    /// <param name="categoryId">The ID of the <see cref="Category"/> to associate with the expense.</param>
    /// <param name="amount">The numerical amount of the expense.</param>
    /// <param name="currency">The currency code of the expense.</param>
    /// <param name="transactionDate">The date and time of the expense transaction.</param>
    /// <param name="walletId">The ID of the <see cref="Wallet"/> from which the expense was made.</param>
    /// <param name="authorId">The ID of the <see cref="Author"/> who created the expense.</param>
    /// <param name="description">The description of the expense.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result is the created <see cref="Expense"/> entity.</returns>
    Task<Expense> CreateExpense(Guid categoryId, decimal amount, string currency, DateTime transactionDate, Guid walletId, Guid authorId, string? description);

    /// <summary>
    /// Creates a new <see cref="Expense"/> instance asynchronously using a <see cref="Money"/> object.
    /// </summary>
    /// <param name="categoryId">The ID of the <see cref="Category"/> to associate with the expense.</param>
    /// <param name="amount">A <see cref="Money"/> object representing the amount and currency of the expense.</param>
    /// <param name="transactionDate">The date and time of the expense transaction.</param>
    /// <param name="walletId">The ID of the <see cref="Wallet"/> from which the expense was made.</param>
    /// <param name="authorId">The ID of the <see cref="Author"/> who created the expense.</param>
    /// <param name="description">The description of the expense.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result is the created <see cref="Expense"/> entity.</returns>
    Task<Expense> CreateExpense(Guid categoryId, Money amount, DateTime transactionDate, Guid walletId, Guid authorId, string? description);
}