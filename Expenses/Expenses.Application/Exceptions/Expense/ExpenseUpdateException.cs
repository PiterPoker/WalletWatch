namespace Expenses.Application.Exceptions.Expense;

/// <summary>
/// Exception thrown when an error occurs during the update of an expense.
/// </summary>
public class ExpenseUpdateException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExpenseUpdateException"/> class with a specified expense ID and error message.
    /// </summary>
    /// <param name="id">The ID of the expense that failed to update.</param>
    /// <param name="message">The message that describes the error during the update.</param>
    public ExpenseUpdateException(Guid id, string message) : base($"Failed to update expense with ID {id}. {message}") { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpenseUpdateException"/> class with a specified expense ID, error message, and inner exception.
    /// </summary>
    /// <param name="id">The ID of the expense that failed to update.</param>
    /// <param name="message">The message that describes the error during the update.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public ExpenseUpdateException(Guid id, string message, Exception innerException) : base($"Failed to update expense with ID {id}. {message}", innerException) { }
}