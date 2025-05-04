using System.ComponentModel.DataAnnotations;

namespace Expenses.Application.DTOs.Wallet;

/// <summary>
/// Data Transfer Object (DTO) for creating a new Wallet.
/// </summary>
public record CreateWalletDto
{
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the name of the Wallet to be created.
    /// </summary>
    /// <remarks>
    /// This property is required and cannot exceed 255 characters.
    /// </remarks>
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
    public required string Name { get; init; }
}