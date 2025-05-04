namespace Expenses.Application.DTOs.Author;

/// <summary>
/// Data Transfer Object (DTO) representing an Author.
/// </summary>
public record AuthorDto
{
    /// <summary>
    /// Gets the unique identifier of the Author.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the name of the Author.
    /// </summary>
    public required string Name { get; init; }
}