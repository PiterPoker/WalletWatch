using Expenses.Application.DTOs.Wallet;

namespace Expenses.Application.Interfaces.Services;

/// <summary>
/// Defines the contract for the Wallet service, providing methods to manage Wallet entities.
/// </summary>
public interface IWalletService
{
    /// <summary>
    /// Asynchronously creates a new Wallet.
    /// </summary>
    /// <param name="dto">The Data Transfer Object containing the Wallet creation details.</param>
    /// <returns>A Task containing the created Wallet's Data Transfer Object.</returns>
    Task<WalletDto?> CreateWalletAsync(CreateWalletDto dto);

    /// <summary>
    /// Asynchronously retrieves a Wallet by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Wallet.</param>
    /// <returns>A Task containing the Wallet's Data Transfer Object, or throws an exception if not found.</returns>
    Task<WalletDto?> GetWalletByIdAsync(Guid id);

    /// <summary>
    /// Asynchronously retrieves all Wallets.
    /// </summary>
    /// <returns>A Task containing a collection of Wallet Data Transfer Objects.</returns>
    Task<IEnumerable<WalletDto>> GetAllWalletsAsync();

    /// <summary>
    /// Asynchronously updates an existing Wallet.
    /// </summary>
    /// <param name="dto">The Data Transfer Object containing the Wallet's updated details.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    Task UpdateWalletAsync(Guid walletId, UpdateWalletDto dto);

    /// <summary>
    /// Asynchronously deletes a Wallet by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Wallet to delete.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    Task DeleteWalletAsync(Guid id);
}