using System.ComponentModel.DataAnnotations;

namespace Expenses.Application.DTOs.Author;

/// <summary>
/// Data Transfer Object (DTO) for creating a new Author.
/// </summary>
public record CreateAuthorDto
{
    /// <summary>
    /// Gets the name of the Author to be created.
    /// </summary>
    /// <remarks>
    /// This property is required and cannot exceed 255 characters.
    /// </remarks>
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
    public required string Name { get; init; }
}