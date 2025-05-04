namespace Expenses.Application.DTOs.Wallet;

/// <summary>
/// Data Transfer Object (DTO) representing a Wallet.
/// </summary>
public record WalletDto
{
    /// <summary>
    /// Gets the unique identifier of the Wallet.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the name of the Wallet.
    /// </summary>
    public required string Name { get; init; }
}