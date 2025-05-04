using System.ComponentModel.DataAnnotations;

namespace Expenses.Application.DTOs.Category;

public record CategoryDto
{
    /// <summary>
    /// Gets the unique identifier of the Category.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the name of the Category.
    /// </summary>
    [Required(ErrorMessage = "Category Name is required.")]
    [StringLength(255, ErrorMessage = "Category Name must be at most 255 characters.")]
    public required string Name { get; init; }

    /// <summary>
    /// Gets the ID of the Author associated with the Category.
    /// </summary>
    public Guid AuthorId { get; init; }

    /// <summary>
    /// Gets the color associated with the Category. Can be null.
    /// </summary>
    public string? Color { get; init; }
}