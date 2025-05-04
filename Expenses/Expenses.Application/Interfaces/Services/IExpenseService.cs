using Expenses.Application.DTOs.Expense;

namespace Expenses.Application.Interfaces.Services;

/// <summary>
/// Defines the contract for the Expense service, providing methods to manage Expense entities.
/// </summary>
public interface IExpenseService
{
    Task<List<ExpenseDto>> GetExpensesByAuthorsAsync(List<Guid> authorIds);
    /// <summary>
    /// Asynchronously creates a new expense.
    /// </summary>
    /// <param name="createExpenseDto">The Data Transfer Object containing the expense creation details.</param>
    /// <returns>A Task containing the created Expense's Data Transfer Object, or null if creation failed.</returns>
    Task<ExpenseDto?> CreateExpenseAsync(CreateExpenseDto createExpenseDto);

    /// <summary>
    /// Asynchronously updates an existing expense.
    /// </summary>
    /// <param name="expenseId">The unique identifier of the expense to update.</param>
    /// <param name="updateExpenseDto">The Data Transfer Object containing the expense update details.</param>
    /// <returns>A Task containing the updated Expense's Data Transfer Object, or null if update failed.</returns>
    Task<ExpenseDto?> UpdateExpenseAsync(Guid expenseId, UpdateExpenseDto updateExpenseDto);

    /// <summary>
    /// Asynchronously deletes an expense by its unique identifier.
    /// </summary>
    /// <param name="expenseId">The unique identifier of the expense to delete.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    Task DeleteExpenseAsync(Guid expenseId);

    /// <summary>
    /// Asynchronously retrieves an expense by its unique identifier.
    /// </summary>
    /// <param name="expenseId">The unique identifier of the expense.</param>
    /// <returns>A Task containing the Expense's Data Transfer Object, or null if not found.</returns>
    Task<ExpenseDto?> GetExpenseByIdAsync(Guid expenseId);

    /// <summary>
    /// Asynchronously retrieves a list of expenses associated with a specific category.
    /// </summary>
    /// <param name="categoryId">The unique identifier of the category.</param>
    /// <returns>A Task containing a list of Expense Data Transfer Objects.</returns>
    Task<List<ExpenseDto>> GetExpensesByCategoryIdAsync(Guid categoryId);

    /// <summary>
    /// Asynchronously retrieves a list of expenses within a specified date range.
    /// </summary>
    /// <param name="startDate">The start date of the period.</param>
    /// <param name="endDate">The end date of the period.</param>
    /// <returns>A Task containing a list of Expense Data Transfer Objects.</returns>
    Task<List<ExpenseDto>> GetExpensesByPeriodAsync(DateTime startDate, DateTime endDate);

    /// <summary>
    /// Asynchronously retrieves a list of expenses associated with a specific wallet.
    /// </summary>
    /// <param name="walletId">The unique identifier of the wallet.</param>
    /// <returns>A Task containing a list of Expense Data Transfer Objects.</returns>
    Task<List<ExpenseDto>> GetExpensesByWalletIdAsync(Guid walletId);

    /// <summary>
    /// Asynchronously retrieves a list of all expenses.
    /// </summary>
    /// <returns>A Task containing a list of all Expense Data Transfer Objects.</returns>
    Task<List<ExpenseDto>> GetAllExpensesAsync();
}