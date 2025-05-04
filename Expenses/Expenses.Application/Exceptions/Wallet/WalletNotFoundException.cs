namespace Expenses.Application.Exceptions.Wallet;

/// <summary>
/// Exception thrown when a Wallet is not found.
/// </summary>
public class WalletNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WalletNotFoundException"/> class with the specified wallet ID.
    /// </summary>
    /// <param name="id">The ID of the wallet that was not found.</param>
    public WalletNotFoundException(Guid id) : base($"Wallet with ID {id} was not found.") { }
}