using System.ComponentModel.DataAnnotations;

namespace Expenses.Application.DTOs.Category;

public record UpdateCategoryDto
{
    /// <summary>
    /// Gets the updated name of the Category.
    /// </summary>
    [Required(ErrorMessage = "Category Name is required.")]
    [StringLength(255, ErrorMessage = "Category Name must be at most 255 characters.")]
    public required string Name { get; init; }

    /// <summary>
    /// Gets the updated color associated with the Category. Can be null.
    /// </summary>
    public string? Color { get; init; }
}