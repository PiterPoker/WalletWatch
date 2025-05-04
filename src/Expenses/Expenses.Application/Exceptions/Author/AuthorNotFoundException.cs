namespace Expenses.Application.Exceptions.Author;

/// <summary>
/// Represents an exception that is thrown when an Author is not found.
/// </summary>
public class AuthorNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorNotFoundException"/> class with a specified Author ID.
    /// </summary>
    /// <param name="id">The ID of the Author that was not found.</param>
    public AuthorNotFoundException(Guid id) : base($"Author with id {id} not found.") { }
}