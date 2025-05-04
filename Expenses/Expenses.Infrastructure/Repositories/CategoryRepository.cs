using Expenses.Domain.Entities;
using Expenses.Domain.Interfaces.Repositories;
using Expenses.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Infrastructure.Repositories;

/// <summary>
/// Repository for managing Category entities.
/// Implements the ICategoryRepository interface.
/// </summary>
public class CategoryRepository : ICategoryRepository
{
    private readonly ExpensesContext _context;

    /// <summary>
    /// Constructor for the CategoryRepository.
    /// </summary>
    /// <param name="context">The ExpensesContext database context.</param>
    public CategoryRepository(ExpensesContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Returns the UnitOfWork associated with this repository.
    /// </summary>
    public IUnitOfWork UnitOfWork => _context;

    /// <summary>
    /// Asynchronously adds a Category entity to the database.
    /// </summary>
    /// <param name="entity">The Category entity to add.</param>
    public async Task AddAsync(Category entity)
    {
        await _context.Categories.AddAsync(entity);
    }

    /// <summary>
    /// Deletes a Category entity from the database.
    /// </summary>
    /// <param name="entity">The Category entity to delete.</param>
    public Task DeleteAsync(Category entity)
    {
        _context.Categories.Remove(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Asynchronously returns a list of all Categories from the database.
    /// </summary>
    /// <returns>A list of Category entities.</returns>
    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories.Include(c => c.Author).ToListAsync();
    }

    /// <summary>
    /// Asynchronously returns a Category by ID.
    /// </summary>
    /// <param name="id">The Category ID.</param>
    /// <returns>The Category entity or null if not found.</returns>
    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _context.Categories.Include(c=>c.Author).FirstAsync(c=>c.Id == id);
    }

    /// <summary>
    /// Asynchronously returns a Category by name.
    /// </summary>
    /// <param name="name">The Category name.</param>
    /// <returns>The Category entity or null if not found.</returns>
    public async Task<Category?> GetCategoryByNameAsync(string name)
    {
        return await _context.Categories.Include(c => c.Author).FirstOrDefaultAsync(c => c.Name == name);
    }

    /// <summary>
    /// Updates a Category entity in the database.
    /// </summary>
    /// <param name="entity">The Category entity to update.</param>
    public Task UpdateAsync(Category entity)
    {
        _context.Categories.Update(entity);
        return Task.CompletedTask;
    }
}