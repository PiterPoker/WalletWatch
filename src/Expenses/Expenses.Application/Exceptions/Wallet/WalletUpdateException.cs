namespace Expenses.Application.Exceptions.Wallet;

/// <summary>
/// Exception thrown when an error occurs while updating a Wallet.
/// </summary>
public class WalletUpdateException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WalletUpdateException"/> class with a specified wallet ID and error message.
    /// </summary>
    /// <param name="id">The ID of the wallet that failed to update.</param>
    /// <param name="message">The message that describes the error.</param>
    public WalletUpdateException(Guid id, string message) : base($"Failed to update wallet with ID {id}. {message}") { }

    /// <summary>
    /// Initializes a new instance of the <see cref="WalletUpdateException"/> class with a specified wallet ID, error message, and inner exception.
    /// </summary>
    /// <param name="id">The ID of the wallet that failed to update.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public WalletUpdateException(Guid id, string message, Exception innerException) : base($"Failed to update wallet with ID {id}. {message}", innerException) { }
}