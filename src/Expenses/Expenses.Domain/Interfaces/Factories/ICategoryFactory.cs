using Expenses.Domain.Entities;
using System.Drawing;

namespace Expenses.Domain.Interfaces.Factories;

/// <summary>
/// Defines an interface for creating instances of the <see cref="Category"/> entity.
/// </summary>
public interface ICategoryFactory
{
    /// <summary>
    /// Creates a new <see cref="Category"/> instance.
    /// </summary>
    /// <param name="name">The name of the category.</param>
    /// <param name="author">The author of the category.</param>
    /// <param name="color">An optional color for the category.</param>
    /// <returns>A new <see cref="Category"/> instance.</returns>
    Category CreateCategory(string name, Author? author, Color? color);


    /// <summary>
    /// Creates a new <see cref="Category"/> instance.
    /// </summary>
    /// <param name="name">The name of the category.</param>
    /// <param name="authorId">The ID of the author of the category.</param>
    /// <param name="color">An optional color for the category.</param>
    /// <returns>A new <see cref="Category"/> instance.</returns>
    Task<Category> CreateCategoryAsync(string name, Guid authorId, Color? color);
}