using System.ComponentModel.DataAnnotations;

namespace Expenses.Application.DTOs.Author;

/// <summary>
/// Data Transfer Object (DTO) for updating an Author.
/// </summary>
public record UpdateAuthorDto
{
    /// <summary>
    /// Gets or sets the updated name of the Author.
    /// </summary>
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
    public required string Name { get; set; }
}