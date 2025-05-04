using AutoMapper;
using Expenses.Application.DTOs.Author;
using Expenses.Application.Exceptions.Author;
using Expenses.Application.Interfaces.Services;
using Expenses.Domain.Entities;
using Expenses.Domain.Interfaces.Repositories;

namespace Expenses.Application.Services;

/// <summary>
/// Service for managing the Author entity.
/// </summary>
public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor for AuthorService.
    /// </summary>
    /// <param name="authorRepository">Repository for accessing the Author entity.</param>
    /// <param name="mapper">Mapper for object transformation.</param>
    /// <exception cref="ArgumentNullException">Thrown when authorRepository or mapper is null.</exception>
    public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Asynchronously creates a new author.
    /// </summary>
    /// <param name="dto">DTO for creating an author.</param>
    public async Task<AuthorDto?> CreateAuthorAsync(CreateAuthorDto dto)
    {
        var author = _mapper.Map<Author>(dto);
        await _authorRepository.AddAsync(author);
        await _authorRepository.UnitOfWork.SaveEntitiesAsync();
        return _mapper.Map<AuthorDto>(author);
    }

    /// <summary>
    /// Asynchronously retrieves an author by ID.
    /// </summary>
    /// <param name="id">The ID of the author.</param>
    /// <returns>Author DTO or null if the author is not found.</returns>
    /// <exception cref="AuthorNotFoundException">Thrown when the author is not found.</exception>
    public async Task<AuthorDto?> GetAuthorByIdAsync(Guid id)
    {
        var author = await _authorRepository.GetByIdAsync(id);

        return author is null
            ? throw new AuthorNotFoundException(id)
            : _mapper.Map<AuthorDto>(author);
    }

    /// <summary>
    /// Asynchronously updates an author.
    /// </summary>
    /// <param name="dto">DTO for updating an author.</param>
    /// <exception cref="AuthorNotFoundException">Thrown when the author is not found.</exception>
    /// <exception cref="AuthorUpdateException">Thrown when an error occurs while updating the author.</exception>
    public async Task UpdateAuthorAsync(Guid authorId, UpdateAuthorDto dto)
    {
        try
        {
            var author = await _authorRepository.GetByIdAsync(authorId) ?? throw new AuthorNotFoundException(authorId);
            author.UpdateName(dto.Name);
            await _authorRepository.UpdateAsync(author);
            await _authorRepository.UnitOfWork.SaveEntitiesAsync();
        }
        catch (AuthorNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new AuthorUpdateException(authorId, "An error occurred while updating the author.", ex);
        }
    }

    /// <summary>
    /// Asynchronously deletes an author.
    /// </summary>
    /// <param name="id">The ID of the author.</param>
    /// <exception cref="AuthorNotFoundException">Thrown when the author is not found.</exception>
    /// <exception cref="AuthorDeleteException">Thrown when an error occurs while deleting the author.</exception>
    public async Task DeleteAuthorAsync(Guid id)
    {
        try
        {
            var author = await _authorRepository.GetByIdAsync(id) ?? throw new AuthorNotFoundException(id);
            await _authorRepository.DeleteAsync(author);
            await _authorRepository.UnitOfWork.SaveEntitiesAsync();
        }
        catch (AuthorNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new AuthorDeleteException(id, "An error occurred while deleting the author.", ex);
        }
    }
}