using Expenses.Domain.Entities;
using Expenses.Domain.Interfaces.Factories;
using Expenses.Domain.Interfaces.Repositories;
using System.Drawing;

namespace Expenses.Domain.Implementations.Factories;

/// <summary>
/// Implementation of the <see cref="ICategoryFactory"/> interface for creating <see cref="Category"/> entities.
/// </summary>
public class CategoryFactory : ICategoryFactory
{
    private readonly IAuthorRepository _authorRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryFactory"/> class.
    /// </summary>
    /// <param name="authorRepository">The repository for accessing <see cref="Author"/> entities.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="authorRepository"/> is <c>null</c>.</exception>
    public CategoryFactory(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
    }

    /// <summary>
    /// Creates a new <see cref="Category"/> entity.
    /// </summary>
    /// <param name="name">The name of the category.</param>
    /// <param name="author">The <see cref="Author"/> entity associated with the category.</param>
    /// <param name="color">An optional <see cref="Color"/> for the category.</param>
    /// <returns>A new <see cref="Category"/> entity.</returns>
    public Category CreateCategory(string name, Author? author, Color? color)
    {
        return new Category(name, author, color);
    }

    /// <summary>
    /// Creates a new <see cref="Category"/> entity asynchronously.
    /// </summary>
    /// <param name="name">The name of the category.</param>
    /// <param name="authorId">The ID of the <see cref="Author"/> associated with the category.</param>
    /// <param name="color">An optional <see cref="Color"/> for the category.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result is the created <see cref="Category"/> entity.</returns>
    /// <exception cref="ArgumentException">Thrown when an author with the specified ID is not found.</exception>
    public async Task<Category> CreateCategoryAsync(string name, Guid authorId, Color? color)
    {
        var author = await _authorRepository.GetByIdAsync(authorId);

        return CreateCategory(name, author, color);
    }
}