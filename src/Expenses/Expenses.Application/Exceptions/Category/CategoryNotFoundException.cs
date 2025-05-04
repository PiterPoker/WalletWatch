namespace Expenses.Application.Exceptions.Category;

/// <summary>
/// Exception thrown when a Category is not found.
/// </summary>
public class CategoryNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryNotFoundException"/> class with a specified category ID.
    /// </summary>
    /// <param name="id">The ID of the category that was not found.</param>
    public CategoryNotFoundException(Guid id) : base($"Category with ID {id} not found.") { }
}