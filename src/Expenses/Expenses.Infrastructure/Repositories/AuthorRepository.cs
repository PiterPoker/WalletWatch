using Expenses.Domain.Entities;
using Expenses.Domain.Interfaces.Repositories;
using Expenses.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Infrastructure.Repositories;

/// <summary>
/// Repository for managing Author entities.
/// Implements the IAuthorRepository interface.
/// </summary>
public class AuthorRepository : IAuthorRepository
{
    private readonly ExpensesContext _context;

    /// <summary>
    /// Constructor for the AuthorRepository.
    /// </summary>
    /// <param name="context">The ExpensesContext database context.</param>
    public AuthorRepository(ExpensesContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Returns the UnitOfWork associated with this repository.
    /// </summary>
    public IUnitOfWork UnitOfWork => _context;

    /// <summary>
    /// Asynchronously adds an Author entity to the database.
    /// </summary>
    /// <param name="entity">The Author entity to add.</param>
    public async Task AddAsync(Author entity)
    {
        await _context.Authors.AddAsync(entity);
    }

    /// <summary>
    /// Deletes an Author entity from the database.
    /// </summary>
    /// <param name="entity">The Author entity to delete.</param>
    public Task DeleteAsync(Author entity)
    {
        _context.Authors.Remove(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Asynchronously returns a list of all authors from the database.
    /// </summary>
    /// <returns>A list of Author entities.</returns>
    public async Task<List<Author>> GetAllAsync()
    {
        return await _context.Authors.ToListAsync();
    }

    /// <summary>
    /// Asynchronously returns an author by name.
    /// </summary>
    /// <param name="name">The author's name.</param>
    /// <returns>The Author entity or null if not found.</returns>
    public async Task<Author?> GetAuthorByNameAsync(string name)
    {
        return await _context.Authors.FirstOrDefaultAsync(a => a.Name == name);
    }

    /// <summary>
    /// Asynchronously returns an author by ID.
    /// </summary>
    /// <param name="id">The author's ID.</param>
    /// <returns>The Author entity or null if not found.</returns>
    public async Task<Author?> GetByIdAsync(Guid id)
    {
        return await _context.Authors.FindAsync(id);
    }

    /// <summary>
    /// Updates an Author entity in the database.
    /// </summary>
    /// <param name="entity">The Author entity to update.</param>
    public Task UpdateAsync(Author entity)
    {
        _context.Authors.Update(entity);
        return Task.CompletedTask;
    }
}