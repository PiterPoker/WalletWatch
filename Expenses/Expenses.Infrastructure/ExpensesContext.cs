using Expenses.Domain.Entities;
using Expenses.Domain.SeedWork;
using Expenses.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Expenses.Infrastructure;

/// <summary>
/// Database context for the expenses management application.
/// Implements the IUnitOfWork interface for transaction management.
/// </summary>
public class ExpensesContext : DbContext, IUnitOfWork
{
    /// <summary>
    /// Database context constructor.
    /// </summary>
    /// <param name="options">Database context configuration options.</param>
    public ExpensesContext(DbContextOptions<ExpensesContext> options) : base(options) 
    {
        Database.EnsureCreated();
    }

    /// <summary>
    /// Data set for the Expense entity.
    /// </summary>
    public DbSet<Expense> Expenses { get; set; }

    /// <summary>
    /// Data set for the Category entity.
    /// </summary>
    public DbSet<Category> Categories { get; set; }

    /// <summary>
    /// Data set for the Wallet entity.
    /// </summary>
    public DbSet<Wallet> Wallets { get; set; }

    /// <summary>
    /// Data set for the Author entity.
    /// </summary>
    public DbSet<Author> Authors { get; set; }

    /// <summary>
    /// Saves changes to the database.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if changes are saved successfully.</returns>
    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _ = await base.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new UnitOfWorkException("Error saving changes.", ex);
        }
        catch (Exception ex)
        {
            throw new UnitOfWorkException("An unexpected error occurred while saving changes.", ex);
        }
    }

    /// <summary>
    /// Configures the database model.
    /// </summary>
    /// <param name="modelBuilder">Model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("expense");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExpensesContext).Assembly);
    }

    private IDbContextTransaction? _currentTransaction;

    /// <summary>
    /// Returns the currently active transaction.
    /// </summary>
    /// <returns>The current transaction or null if no transaction is active.</returns>
    public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;

    /// <summary>
    /// Checks if there is an active transaction.
    /// </summary>
    public bool HasActiveTransaction => _currentTransaction != null;

    /// <summary>
    /// Begins a new transaction.
    /// </summary>
    public async Task BeginTransactionAsync()
    {
        _currentTransaction = await this.Database.BeginTransactionAsync();
    }

    /// <summary>
    /// Commits the current transaction.
    /// </summary>
    public async Task CommitTransactionAsync()
    {
        try
        {
            if (_currentTransaction is not null)
                await _currentTransaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await RollbackTransactionAsync();
            throw new UnitOfWorkException("Error committing transaction.", ex);
        }
    }

    /// <summary>
    /// Rolls back the current transaction.
    /// </summary>
    public async Task RollbackTransactionAsync()
    {
        try
        {
            if (_currentTransaction is not null)
                await _currentTransaction.RollbackAsync();
        }
        catch (Exception ex)
        {
            throw new UnitOfWorkException("Error rolling back transaction.", ex);
        }
    }
}