namespace Expenses.Application.Exceptions.Category;

/// <summary>
/// Exception thrown when an error occurs while updating a Category.
/// </summary>
public class CategoryUpdateException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryUpdateException"/> class with a specified category ID and error message.
    /// </summary>
    /// <param name="id">The ID of the category that failed to update.</param>
    /// <param name="message">The message that describes the error.</param>
    public CategoryUpdateException(Guid id, string message) : base($"Failed to update category with ID {id}. {message}") { }

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryUpdateException"/> class with a specified category ID, error message, and inner exception.
    /// </summary>
    /// <param name="id">The ID of the category that failed to update.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public CategoryUpdateException(Guid id, string message, Exception innerException) : base($"Failed to update category with ID {id}. {message}", innerException) { }
}