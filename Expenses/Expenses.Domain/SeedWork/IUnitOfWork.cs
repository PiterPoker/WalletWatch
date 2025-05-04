namespace Expenses.Domain.SeedWork;

/// <summary>
/// Interface for managing a unit of work, ensuring atomicity of database operations.
/// Inherits the IDisposable interface to release resources.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Saves all changes made in the database context asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>The number of state entries written to the underlying database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves all changes made in the database context asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>True if changes were saved successfully, otherwise false.</returns>
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begins a new transaction asynchronously.
    /// </summary>
    Task BeginTransactionAsync();

    /// <summary>
    /// Commits the current transaction asynchronously.
    /// </summary>
    Task CommitTransactionAsync();

    /// <summary>
    /// Rolls back the current transaction asynchronously.
    /// </summary>
    Task RollbackTransactionAsync();
}