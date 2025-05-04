using System.ComponentModel.DataAnnotations;

namespace Expenses.Application.DTOs.Wallet;

/// <summary>
/// Data Transfer Object (DTO) for updating a Wallet.
/// </summary>
public record UpdateWalletDto
{
    /// <summary>
    /// Gets the updated name of the Wallet.
    /// </summary>
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
    public required string Name { get; init; }
}