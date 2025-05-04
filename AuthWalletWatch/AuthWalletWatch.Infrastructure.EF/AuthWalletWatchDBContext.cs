using AuthWalletWatch.Infrastructure.EF.Configurations;
using AuthWalletWatch.Infrastructure.EF.Exceptions;
using AuthWalletWatch.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AuthWalletWatch.Infrastructure.EF;

public class AuthWalletWatchDBContext
    : IdentityDbContext<ApplicationUser
        , ApplicationRole
        , Guid
        , ApplicationUserClaim
        , ApplicationUserRole
        , ApplicationUserLogin
        , ApplicationRoleClaim
        , ApplicationUserToken>
{
    public AuthWalletWatchDBContext(DbContextOptions<AuthWalletWatchDBContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationProfile> Profiles { get; set; }

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
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationProfileConfiguration());
        builder.ApplyConfiguration(new ApplicationRoleClaimConfiguration());
        builder.ApplyConfiguration(new ApplicationRoleConfiguration());
        builder.ApplyConfiguration(new ApplicationUserClaimConfiguration());
        builder.ApplyConfiguration(new ApplicationUserConfiguration());
        builder.ApplyConfiguration(new ApplicationUserLoginConfiguration());
        builder.ApplyConfiguration(new ApplicationUserTokenConfiguration());
    }
}
