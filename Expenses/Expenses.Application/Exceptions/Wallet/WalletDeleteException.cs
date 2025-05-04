namespace Expenses.Application.Exceptions.Wallet;

/// <summary>
/// Exception thrown when an error occurs while deleting a Wallet.
/// </summary>
public class WalletDeleteException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WalletDeleteException"/> class with a specified wallet ID and error message.
    /// </summary>
    /// <param name="id">The ID of the wallet that failed to be deleted.</param>
    /// <param name="message">The message that describes the error.</param>
    public WalletDeleteException(Guid id, string message) : base($"Failed to delete wallet with ID {id}. {message}") { }

    /// <summary>
    /// Initializes a new instance of the <see cref="WalletDeleteException"/> class with a specified wallet ID, error message, and inner exception.
    /// </summary>
    /// <param name="id">The ID of the wallet that failed to be deleted.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public WalletDeleteException(Guid id, string message, Exception innerException) : base($"Failed to delete wallet with ID {id}. {message}", innerException) { }
}