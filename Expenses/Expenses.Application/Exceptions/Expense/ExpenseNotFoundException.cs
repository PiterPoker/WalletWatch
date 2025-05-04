namespace Expenses.Application.Exceptions.Expense;

/// <summary>
/// Exception thrown when an expense is not found.
/// </summary>
public class ExpenseNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the ExpenseNotFoundException class.
    /// </summary>
    public ExpenseNotFoundException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the ExpenseNotFoundException class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that describes the reason for the exception.</param>
    public ExpenseNotFoundException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the ExpenseNotFoundException class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that describes the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public ExpenseNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the ExpenseNotFoundException class with the expense ID.
    /// </summary>
    /// <param name="expenseId">The ID of the expense that was not found.</param>
    public ExpenseNotFoundException(Guid expenseId)
        : base($"Expense with ID {expenseId} not found.")
    {
    }
}
