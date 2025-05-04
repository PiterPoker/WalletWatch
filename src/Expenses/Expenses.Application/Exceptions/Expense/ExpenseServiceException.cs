namespace Expenses.Application.Exceptions.Expense;

/// <summary>
/// Represents exceptions that occur within the ExpenseService in the application layer.
/// This exception is used to encapsulate and propagate errors related to business logic
/// operations concerning expenses.
/// </summary>
public class ExpenseServiceException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExpenseServiceException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
    /// </param>
    public ExpenseServiceException(string message, Exception? innerException) : base(message, innerException) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpenseServiceException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public ExpenseServiceException(string message) : base(message) { }
}