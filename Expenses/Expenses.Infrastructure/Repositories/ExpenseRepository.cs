using Expenses.Domain.Entities;
using Expenses.Domain.Interfaces.Repositories;
using Expenses.Domain.Interfaces.Specifications;
using Expenses.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Infrastructure.Repositories;

/// <summary>
/// Repository for managing Expense entities.
/// Implements the IExpenseRepository interface.
/// </summary>
public class ExpenseRepository : IExpenseRepository
{
    private readonly ExpensesContext _context;

    /// <summary>
    /// Конструктор репозитория ExpenseRepository.
    /// </summary>
    /// <param name="context">Контекст базы данных ExpensesContext.</param>
    public ExpenseRepository(ExpensesContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<Expense>> GetExpensesAsync(ISpecification<Expense> specification)
    {
        var query = _context.Expenses.AsQueryable();

        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }

        if (specification.Includes is not null)
        {
            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return await query.ToListAsync();
    }

    /// <summary>
    /// Returns the UnitOfWork associated with this repository.
    /// </summary>
    public IUnitOfWork UnitOfWork => _context;

    /// <summary>
    /// Asynchronously adds an Expense entity to the database.
    /// </summary>
    /// <param name="entity">The Expense entity to add.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task AddAsync(Expense entity)
    {
        await _context.Expenses.AddAsync(entity);
    }

    /// <summary>
    /// Asynchronously deletes an Expense entity from the database.
    /// </summary>
    /// <param name="entity">The Expense entity to delete.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public Task DeleteAsync(Expense entity)
    {
        _context.Expenses.Remove(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Asynchronously retrieves a list of all Expense entities from the database.
    /// </summary>
    /// <returns>A Task containing a list of Expense entities.</returns>
    public async Task<List<Expense>> GetAllAsync()
    {
        return await _context.Expenses.Include(e => e.Wallet).Include(e => e.Author).Include(e => e.Category).ThenInclude(eC => eC.Author).ToListAsync();
    }

    /// <summary>
    /// Asynchronously retrieves an Expense entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the Expense entity.</param>
    /// <returns>A Task containing the Expense entity or null if not found.</returns>
    public async Task<Expense?> GetByIdAsync(Guid id)
    {
        return await _context.Expenses.Include(e => e.Wallet).Include(e => e.Author).Include(e => e.Category).ThenInclude(eC => eC.Author).FirstAsync(c => c.Id == id);
    }

    /// <summary>
    /// Asynchronously retrieves a list of Expense entities by category ID.
    /// </summary>
    /// <param name="categoryId">The ID of the category.</param>
    /// <returns>A Task containing a list of Expense entities.</returns>
    public async Task<List<Expense>> GetExpensesByCategoryIdAsync(Guid categoryId)
    {
        return await _context.Expenses.Include(e => e.Wallet).Include(e => e.Author).Include(e => e.Category).ThenInclude(eC => eC.Author).Where(e => e.Category.Id == categoryId).ToListAsync();
    }

    /// <summary>
    /// Asynchronously retrieves a list of Expense entities within a specified date range.
    /// </summary>
    /// <param name="startDate">The start date of the period.</param>
    /// <param name="endDate">The end date of the period.</param>
    /// <returns>A Task containing a list of Expense entities.</returns>
    public async Task<List<Expense>> GetExpensesByPeriodAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Expenses.Include(e => e.Wallet).Include(e => e.Author).Include(e => e.Category).ThenInclude(eC => eC.Author).Where(e => e.TransactionDate >= startDate && e.TransactionDate <= endDate).ToListAsync();
    }

    /// <summary>
    /// Asynchronously retrieves a list of Expense entities by wallet ID.
    /// </summary>
    /// <param name="walletId">The ID of the wallet.</param>
    /// <returns>A Task containing a list of Expense entities.</returns>
    public async Task<List<Expense>> GetExpensesByWalletIdAsync(Guid walletId)
    {
        return await _context.Expenses.Include(e => e.Wallet).Include(e => e.Author).Include(e => e.Category).ThenInclude(eC => eC.Author).Where(e => e.Wallet.Id == walletId).ToListAsync();
    }

    /// <summary>
    /// Asynchronously updates an Expense entity in the database.
    /// </summary>
    /// <param name="entity">The Expense entity to update.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public Task UpdateAsync(Expense entity)
    {
        _context.Expenses.Update(entity);
        return Task.CompletedTask;
    }
}