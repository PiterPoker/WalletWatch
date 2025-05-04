using AutoMapper;
using Expenses.Application.DTOs.Expense;
using Expenses.Application.Exceptions.Expense;
using Expenses.Application.Interfaces.Services;
using Expenses.Domain.Entities;
using Expenses.Domain.Interfaces.Factories;
using Expenses.Domain.Interfaces.Repositories;
using Expenses.Domain.Specifications;

namespace Expenses.Application.Services;

/// <summary>
/// Service for managing expense entities.
/// </summary>
public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IExpenseFactory _expenseFactory;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpenseService"/> class.
    /// </summary>
    /// <param name="expenseRepository">The repository for accessing expense data.</param>
    /// <param name="expenseFactory">The factory for creating expense entities.</param>
    /// <param name="categoryRepository">The repository for accessing category data.</param>
    /// <param name="mapper">The AutoMapper instance for object mapping.</param>
    /// <exception cref="ArgumentNullException">Thrown when any of the constructor parameters is null.</exception>
    public ExpenseService(IExpenseRepository expenseRepository, IExpenseFactory expenseFactory, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository ?? throw new ArgumentNullException(nameof(expenseRepository));
        _expenseFactory = expenseFactory ?? throw new ArgumentNullException(nameof(expenseFactory));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<ExpenseDto>> GetExpensesByAuthorsAsync(List<Guid> authorIds)
    {
        var specification = new ExpensesByAuthorsSpecification(authorIds);
        var expenses = await _expenseRepository.GetExpensesAsync(specification);
        return _mapper.Map<List<ExpenseDto>>(expenses);
    }

    /// <summary>
    /// Asynchronously creates a new expense.
    /// </summary>
    /// <param name="createExpenseDto">The DTO containing the expense creation data.</param>
    /// <returns>The created expense DTO, or null if an error occurs.</returns>
    /// <exception cref="Exception">Thrown when an unexpected error occurs during expense creation.</exception>
    public async Task<ExpenseDto?> CreateExpenseAsync(CreateExpenseDto createExpenseDto)
    {
        try
        {
            var expense = await _expenseFactory.CreateExpense(createExpenseDto.CategoryId, createExpenseDto.Amount, createExpenseDto.Currency, createExpenseDto.TransactionDate, createExpenseDto.WalletId, createExpenseDto.AuthorId, createExpenseDto.Description);
            await _expenseRepository.AddAsync(expense);
            await _expenseRepository.UnitOfWork.SaveEntitiesAsync();
            return _mapper.Map<ExpenseDto>(expense);
        }
        catch (Exception ex)
        {
            throw new ExpenseServiceException(ex.Message, ex.InnerException);
        }
    }

    /// <summary>
    /// Asynchronously updates an existing expense.
    /// </summary>
    /// <param name="expenseId">The ID of the expense to update.</param>
    /// <param name="updateExpenseDto">The DTO containing the expense update data.</param>
    /// <returns>The updated expense DTO, or null if an error occurs.</returns>
    /// <exception cref="ExpenseNotFoundException">Thrown when the expense with the specified ID is not found.</exception>
    /// <exception cref="Exception">Thrown when an unexpected error occurs during expense update.</exception>
    public async Task<ExpenseDto?> UpdateExpenseAsync(Guid expenseId, UpdateExpenseDto updateExpenseDto)
    {
        try
        {
            var expense = await _expenseRepository.GetByIdAsync(expenseId) ?? throw new ExpenseNotFoundException(expenseId);
            var category = await _categoryRepository.GetByIdAsync(updateExpenseDto.CategoryId);
            expense.Update(new Money(updateExpenseDto.Amount, updateExpenseDto.Currency), updateExpenseDto.TransactionDate, category, updateExpenseDto.Description);
            await _expenseRepository.UpdateAsync(expense);
            await _expenseRepository.UnitOfWork.SaveEntitiesAsync();
            return _mapper.Map<ExpenseDto>(expense);
        }
        catch (ExpenseNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ExpenseUpdateException(expenseId, ex.Message);
        }
    }

    /// <summary>
    /// Asynchronously deletes an expense.
    /// </summary>
    /// <param name="expenseId">The ID of the expense to delete.</param>
    /// <exception cref="Exception">Thrown when an unexpected error occurs during expense deletion.</exception>
    public async Task DeleteExpenseAsync(Guid expenseId)
    {
        try
        {
            var expense = await _expenseRepository.GetByIdAsync(expenseId);
            if (expense is not null)
            {
                await _expenseRepository.DeleteAsync(expense);
                await _expenseRepository.UnitOfWork.SaveEntitiesAsync();
            }
        }
        catch (Exception ex)
        {
            throw new ExpenseDeleteException(expenseId, ex.Message);
        }
    }

    /// <summary>
    /// Asynchronously retrieves an expense by its ID.
    /// </summary>
    /// <param name="expenseId">The ID of the expense to retrieve.</param>
    /// <returns>The expense DTO, or null if not found or an error occurs.</returns>
    /// <exception cref="ExpenseNotFoundException">Thrown when the expense with the specified ID is not found.</exception>
    /// <exception cref="Exception">Thrown when an unexpected error occurs during expense retrieval.</exception>
    public async Task<ExpenseDto?> GetExpenseByIdAsync(Guid expenseId)
    {
        try
        {
            var expense = await _expenseRepository.GetByIdAsync(expenseId) ?? throw new ExpenseNotFoundException(expenseId);
            return _mapper.Map<ExpenseDto>(expense);
        }
        catch (ExpenseNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ExpenseServiceException(ex.Message, ex.InnerException);
        }
    }

    /// <summary>
    /// Asynchronously retrieves a list of expenses by category ID.
    /// </summary>
    /// <param name="categoryId">The ID of the category to filter expenses by.</param>
    /// <returns>A list of expense DTOs.</returns>
    public async Task<List<ExpenseDto>> GetExpensesByCategoryIdAsync(Guid categoryId)
    {
        var expenses = await _expenseRepository.GetExpensesByCategoryIdAsync(categoryId);
        return _mapper.Map<List<ExpenseDto>>(expenses);
    }

    /// <summary>
    /// Asynchronously retrieves a list of expenses within a specified date range.
    /// </summary>
    /// <param name="startDate">The start date of the date range.</param>
    /// <param name="endDate">The end date of the date range.</param>
    /// <returns>A list of expense DTOs.</returns>
    public async Task<List<ExpenseDto>> GetExpensesByPeriodAsync(DateTime startDate, DateTime endDate)
    {
        var expenses = await _expenseRepository.GetExpensesByPeriodAsync(startDate, endDate);
        return _mapper.Map<List<ExpenseDto>>(expenses);
    }

    /// <summary>
    /// Asynchronously retrieves a list of expenses by wallet ID.
    /// </summary>
    /// <param name="walletId">The ID of the wallet to filter expenses by.</param>
    /// <returns>A list of expense DTOs.</returns>
    public async Task<List<ExpenseDto>> GetExpensesByWalletIdAsync(Guid walletId)
    {
        var expenses = await _expenseRepository.GetExpensesByWalletIdAsync(walletId);
        return _mapper.Map<List<ExpenseDto>>(expenses);
    }

    /// <summary>
    /// Asynchronously retrieves a list of all expenses.
    /// </summary>
    /// <returns>A list of expense DTOs.</returns>
    public async Task<List<ExpenseDto>> GetAllExpensesAsync()
    {
        var expenses = await _expenseRepository.GetAllAsync();
        return _mapper.Map<List<ExpenseDto>>(expenses);
    }
}