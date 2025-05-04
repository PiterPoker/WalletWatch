using Expenses.Domain.Entities;
using Expenses.Domain.Interfaces.Repositories;
using Expenses.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Infrastructure.Repositories;

/// <summary>
/// Repository for managing Wallet entities.
/// Implements the IWalletRepository interface.
/// </summary>
public class WalletRepository : IWalletRepository
{
    private readonly ExpensesContext _context;

    /// <summary>
    /// Constructor for the WalletRepository.
    /// </summary>
    /// <param name="context">The ExpensesContext database context.</param>
    public WalletRepository(ExpensesContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Returns the UnitOfWork associated with this repository.
    /// </summary>
    public IUnitOfWork UnitOfWork => _context;

    /// <summary>
    /// Asynchronously adds a Wallet entity to the database.
    /// </summary>
    /// <param name="entity">The Wallet entity to add.</param>
    public async Task AddAsync(Wallet entity)
    {
        await _context.Wallets.AddAsync(entity);
    }

    /// <summary>
    /// Deletes a Wallet entity from the database.
    /// </summary>
    /// <param name="entity">The Wallet entity to delete.</param>
    public Task DeleteAsync(Wallet entity)
    {
        _context.Wallets.Remove(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Asynchronously returns a list of all Wallets from the database.
    /// </summary>
    /// <returns>A list of Wallet entities.</returns>
    public async Task<List<Wallet>> GetAllAsync()
    {
        return await _context.Wallets.ToListAsync();
    }

    /// <summary>
    /// Asynchronously returns a Wallet by ID.
    /// </summary>
    /// <param name="id">The Wallet ID.</param>
    /// <returns>The Wallet entity or null if not found.</returns>
    public async Task<Wallet?> GetByIdAsync(Guid id)
    {
        return await _context.Wallets.FindAsync(id);
    }

    /// <summary>
    /// Asynchronously returns a Wallet by name.
    /// </summary>
    /// <param name="name">The Wallet name.</param>
    /// <returns>The Wallet entity or null if not found.</returns>
    public async Task<Wallet?> GetWalletByNameAsync(string name)
    {
        return await _context.Wallets.FirstOrDefaultAsync(w => w.Name == name);
    }

    /// <summary>
    /// Updates a Wallet entity in the database.
    /// </summary>
    /// <param name="entity">The Wallet entity to update.</param>
    public Task UpdateAsync(Wallet entity)
    {
        _context.Wallets.Update(entity);
        return Task.CompletedTask;
    }
}