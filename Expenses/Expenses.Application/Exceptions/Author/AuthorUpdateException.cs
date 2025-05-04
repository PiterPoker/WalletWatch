namespace Expenses.Application.Exceptions.Author;

/// <summary>
/// Represents an exception that occurs during the update of an Author entity.
/// </summary>
public class AuthorUpdateException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorUpdateException"/> class with a specified error message and Author ID.
    /// </summary>
    /// <param name="id">The ID of the Author that failed to update.</param>
    /// <param name="message">The message that describes the error.</param>
    public AuthorUpdateException(Guid id, string message) : base($"Failed to update author with id {id}. {message}") { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorUpdateException"/> class with a specified error message, Author ID, and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="id">The ID of the Author that failed to update.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public AuthorUpdateException(Guid id, string message, Exception innerException) : base($"Failed to update author with id {id}. {message}", innerException) { }
}