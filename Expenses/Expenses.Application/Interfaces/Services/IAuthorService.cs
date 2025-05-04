using Expenses.Application.DTOs.Author;
using Expenses.Application.DTOs.Category;

namespace Expenses.Application.Interfaces.Services;

/// <summary>
/// Defines the contract for the Author service, providing methods to manage Author entities.
/// </summary>
public interface IAuthorService
{
    /// <summary>
    /// Asynchronously creates a new Author.
    /// </summary>
    /// <param name="dto">The Data Transfer Object containing the Author's creation details.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    Task<AuthorDto?> CreateAuthorAsync(CreateAuthorDto dto);

    /// <summary>
    /// Asynchronously retrieves an Author by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Author.</param>
    /// <returns>A Task containing the Author's Data Transfer Object, or null if not found.</returns>
    Task<AuthorDto?> GetAuthorByIdAsync(Guid id);

    /// <summary>
    /// Asynchronously updates an existing Author.
    /// </summary>
    /// <param name="dto">The Data Transfer Object containing the Author's updated details.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    Task UpdateAuthorAsync(Guid authorId, UpdateAuthorDto dto);

    /// <summary>
    /// Asynchronously deletes an Author by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Author to delete.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    Task DeleteAuthorAsync(Guid id);
}