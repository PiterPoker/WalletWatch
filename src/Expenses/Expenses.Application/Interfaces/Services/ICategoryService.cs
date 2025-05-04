namespace Expenses.Application.Interfaces.Services;

using Expenses.Application.DTOs.Category;

/// <summary>
/// Defines the contract for the Category service, providing methods to manage Category entities.
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// Asynchronously creates a new Category.
    /// </summary>
    /// <param name="dto">The Data Transfer Object containing the Category creation details.</param>
    /// <returns>A Task containing the created Category's Data Transfer Object, or null if creation failed.</returns>
    Task<CategoryDto?> CreateCategoryAsync(CreateCategoryDto dto);

    /// <summary>
    /// Asynchronously retrieves a Category by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Category.</param>
    /// <returns>A Task containing the Category's Data Transfer Object, or null if not found.</returns>
    Task<CategoryDto?> GetCategoryByIdAsync(Guid id);

    /// <summary>
    /// Asynchronously retrieves all Categories.
    /// </summary>
    /// <returns>A Task containing a collection of Category Data Transfer Objects.</returns>
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();

    /// <summary>
    /// Asynchronously updates an existing Category.
    /// </summary>
    /// <param name="id">The unique identifier of the Category to update.</param>
    /// <param name="dto">The Data Transfer Object containing the Category's updated details.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    Task UpdateCategoryAsync(Guid id, UpdateCategoryDto dto);

    /// <summary>
    /// Asynchronously deletes a Category by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Category to delete.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    Task DeleteCategoryAsync(Guid id);
}