using Expenses.Domain.Entities;
using Expenses.Domain.Interfaces.Factories;
using Expenses.Domain.Interfaces.Repositories;

namespace Expenses.Domain.Implementations.Factories;

/// <summary>
/// Implementation of the <see cref="IExpenseFactory"/> interface for creating <see cref="Expense"/> entities.
/// </summary>
public class ExpenseFactory : IExpenseFactory
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IWalletRepository _walletRepository;
    private readonly ICategoryRepository _categoryRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpenseFactory"/> class.
    /// </summary>
    /// <param name="authorRepository">The repository for accessing <see cref="Author"/> entities.</param>
    /// <param name="walletRepository">The repository for accessing <see cref="Wallet"/> entities.</param>
    /// <exception cref="ArgumentNullException">Thrown when either <paramref name="authorRepository"/> or <paramref name="walletRepository"/> is <c>null</c>.</exception>
    public ExpenseFactory(IAuthorRepository authorRepository, IWalletRepository walletRepository, ICategoryRepository categoryRepository)
    {
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    /// <summary>
    /// Creates a new <see cref="Expense"/> entity asynchronously.
    /// </summary>
    /// <param name="categoryId">The ID of the <see cref="Category"/> to associate with the expense.</param>
    /// <param name="amount">The amount of the expense.</param>
    /// <param name="currency">The currency of the expense.</param>
    /// <param name="transactionDate">The date and time of the expense.</param>
    /// <param name="walletId">The ID of the <see cref="Wallet"/> to associate with the expense.</param>
    /// <param name="authorId">The ID of the <see cref="Author"/> who created the expense.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result is the created <see cref="Expense"/> entity.</returns>
    public async Task<Expense> CreateExpense(Guid categoryId, decimal amount, string currency, DateTime transactionDate, Guid walletId, Guid authorId)
    {
        var money = new Money(amount, currency);
        return await CreateExpense(categoryId, money, transactionDate, walletId, authorId, string.Empty);
    }

    /// <summary>
    /// Creates a new <see cref="Expense"/> entity asynchronously with a description.
    /// </summary>
    /// <param name="categoryId">The ID of the <see cref="Category"/> to associate with the expense.</param>
    /// <param name="amount">The amount of the expense.</param>
    /// <param name="currency">The currency of the expense.</param>
    /// <param name="transactionDate">The date and time of the expense.</param>
    /// <param name="walletId">The ID of the <see cref="Wallet"/> to associate with the expense.</param>
    /// <param name="authorId">The ID of the <see cref="Author"/> who created the expense.</param>
    /// <param name="description">The description of the expense.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result is the created <see cref="Expense"/> entity.</returns>
    public async Task<Expense> CreateExpense(Guid categoryId, decimal amount, string currency, DateTime transactionDate, Guid walletId, Guid authorId, string? description)
    {
        var money = new Money(amount, currency);
        return await CreateExpense(categoryId, money, transactionDate, walletId, authorId, description);
    }

    /// <summary>
    /// Creates a new <see cref="Expense"/> entity asynchronously using a <see cref="Money"/> object for the amount.
    /// </summary>
    /// <param name="categoryId">The ID of the <see cref="Category"/> to associate with the expense.</param>
    /// <param name="amount">A <see cref="Money"/> object representing the amount and currency of the expense.</param>
    /// <param name="transactionDate">The date and time of the expense.</param>
    /// <param name="walletId">The ID of the <see cref="Wallet"/> to associate with the expense.</param>
    /// <param name="authorId">The ID of the <see cref="Author"/> who created the expense.</param>
    /// <param name="description">The description of the expense.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result is the created <see cref="Expense"/> entity.</returns>
    public async Task<Expense> CreateExpense(Guid categoryId, Money amount, DateTime transactionDate, Guid walletId, Guid authorId, string? description)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        var wallet = await _walletRepository.GetByIdAsync(walletId);
        var author = await _authorRepository.GetByIdAsync(authorId);

        return new Expense(amount, transactionDate, author, wallet, category, description);
    }
}