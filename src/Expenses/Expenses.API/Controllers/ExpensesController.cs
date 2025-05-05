using Expenses.API.Exceptions;
using Expenses.Application.DTOs.Expense;
using Expenses.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Expenses.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[SwaggerTag("Operations related to expenses")]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _expenseService;
    private readonly ILogger<ExpensesController> _logger;

    public ExpensesController(IExpenseService expenseService, ILogger<ExpensesController> logger)
    {
        _expenseService = expenseService ?? throw new ArgumentNullException(nameof(expenseService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ExpenseDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [SwaggerOperation(Summary = "Creates a new expense")]
    public async Task<ActionResult<ExpenseDto>> CreateExpense([SwaggerRequestBody(Description = "Data for creating a new expense")] CreateExpenseDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _logger.LogInformation("Attempting to create a new expense with description: {ExpenseDescription}", dto.Description);

            var expense = await _expenseService.CreateExpenseAsync(dto);
            if (expense is null)
            {
                _logger.LogWarning("Failed to create expense with description: {ExpenseDescription}", dto.Description);
                return BadRequest();
            }

            _logger.LogInformation("Expense created successfully with ID: {ExpenseId}", expense.Id);
            return CreatedAtAction(nameof(GetExpenseById), new { expenseId = expense.Id }, expense);
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while creating expense with description: {ExpenseDescription}", dto.Description);
            throw;
        }
    }

    [HttpGet("{expenseId:guid}")]
    [ProducesResponseType(typeof(ExpenseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [SwaggerOperation(Summary = "Retrieves an expense by ID")]
    public async Task<ActionResult<ExpenseDto>> GetExpenseById([SwaggerParameter(Description = "The unique identifier of the expense")] Guid expenseId)
    {
        try
        {
            _logger.LogInformation("Attempting to retrieve expense with ID: {ExpenseId}", expenseId);

            var expense = await _expenseService.GetExpenseByIdAsync(expenseId);

            if (expense is null)
            {
                _logger.LogWarning("Expense with ID: {ExpenseId} not found", expenseId);
                return NotFound();
            }

            _logger.LogInformation("Expense retrieved successfully with ID: {ExpenseId}", expenseId);
            return Ok(expense);
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving expense with ID: {ExpenseId}", expenseId);
            throw;
        }
    }

    [HttpPut("{expenseId}")]
    [ProducesResponseType(typeof(ExpenseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [SwaggerOperation(Summary = "Updates an existing expense by ID")]
    public async Task<ActionResult<ExpenseDto>> UpdateExpense(
        [SwaggerParameter(Description = "The unique identifier of the expense to update")] Guid expenseId,
        [SwaggerRequestBody(Description = "Data for updating the expense")] UpdateExpenseDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _logger.LogInformation("Attempting to update expense with ID: {ExpenseId}", expenseId);

            var updatedExpense = await _expenseService.UpdateExpenseAsync(expenseId, dto);

            if (updatedExpense is null)
            {
                _logger.LogWarning("Failed to update expense with ID: {ExpenseId}", expenseId);
                return BadRequest();
            }

            _logger.LogInformation("Expense updated successfully with ID: {ExpenseId}", expenseId);
            return Ok(updatedExpense);
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while updating expense with ID: {ExpenseId}", expenseId);
            throw;
        }
    }

    [HttpDelete("{expenseId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [SwaggerOperation(Summary = "Deletes an expense by ID")]
    public async Task<IActionResult> DeleteExpense([SwaggerParameter(Description = "The unique identifier of the expense to delete")] Guid expenseId)
    {
        try
        {
            _logger.LogInformation("Attempting to delete expense with ID: {ExpenseId}", expenseId);

            await _expenseService.DeleteExpenseAsync(expenseId);

            _logger.LogInformation("Expense deleted successfully with ID: {ExpenseId}", expenseId);
            return NoContent();
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while deleting expense with ID: {ExpenseId}", expenseId);
            throw;
        }
    }

    [HttpGet("authors")]
    [ProducesResponseType(typeof(List<ExpenseDto>), (int)HttpStatusCode.OK)]
    [SwaggerOperation(Summary = "Retrieves expenses by author IDs")]
    public async Task<ActionResult<List<ExpenseDto>>> GetExpensesByAuthors([FromQuery] List<Guid> authorIds)
    {
        try
        {
            _logger.LogInformation("Attempting to retrieve expenses by author IDs");

            var expenses = await _expenseService.GetExpensesByAuthorsAsync(authorIds);

            _logger.LogInformation("Expenses retrieved successfully by author IDs");
            return Ok(expenses);
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving expenses by author IDs");
            throw;
        }
    }

    [HttpGet("category/{categoryId:guid}")]
    [ProducesResponseType(typeof(List<ExpenseDto>), (int)HttpStatusCode.OK)]
    [SwaggerOperation(Summary = "Retrieves expenses by category ID")]
    public async Task<ActionResult<List<ExpenseDto>>> GetExpensesByCategoryId([SwaggerParameter(Description = "The unique identifier of the category")] Guid categoryId)
    {
        try
        {
            _logger.LogInformation("Attempting to retrieve expenses by category ID: {CategoryId}", categoryId);

            var expenses = await _expenseService.GetExpensesByCategoryIdAsync(categoryId);

            _logger.LogInformation("Expenses retrieved successfully by category ID: {CategoryId}", categoryId);
            return Ok(expenses);
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving expenses by category ID: {CategoryId}", categoryId);
            throw;
        }
    }

    [HttpGet("period")]
    [ProducesResponseType(typeof(List<ExpenseDto>), (int)HttpStatusCode.OK)]
    [SwaggerOperation(Summary = "Retrieves expenses by period")]
    public async Task<ActionResult<List<ExpenseDto>>> GetExpensesByPeriod([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        try
        {
            _logger.LogInformation("Attempting to retrieve expenses by period: {StartDate} to {EndDate}", startDate, endDate);

            var expenses = await _expenseService.GetExpensesByPeriodAsync(startDate, endDate);

            _logger.LogInformation("Expenses retrieved successfully by period: {StartDate} to {EndDate}", startDate, endDate);
            return Ok(expenses);
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving expenses by period: {StartDate} to {EndDate}", startDate, endDate);
            throw;
        }
    }

    [HttpGet("wallet/{walletId:guid}")]
    [ProducesResponseType(typeof(List<ExpenseDto>), (int)HttpStatusCode.OK)]
    [SwaggerOperation(Summary = "Retrieves expenses by wallet ID")]
    public async Task<ActionResult<List<ExpenseDto>>> GetExpensesByWalletId([SwaggerParameter(Description = "The unique identifier of the wallet")] Guid walletId)
    {
        try
        {
            _logger.LogInformation("Attempting to retrieve expenses by wallet ID: {WalletId}", walletId);

            var expenses = await _expenseService.GetExpensesByWalletIdAsync(walletId);

            _logger.LogInformation("Expenses retrieved successfully by wallet ID: {WalletId}", walletId);
            return Ok(expenses);
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving expenses by wallet ID: {WalletId}", walletId);
            throw;
        }
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(List<ExpenseDto>), (int)HttpStatusCode.OK)]
    [SwaggerOperation(Summary = "Retrieves all expenses")]
    public async Task<ActionResult<List<ExpenseDto>>> GetAllExpenses()
    {
        try
        {
            _logger.LogInformation("Attempting to retrieve all expenses");

            var expenses = await _expenseService.GetAllExpensesAsync();

            _logger.LogInformation("All expenses retrieved successfully");
            return Ok(expenses);
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving all expenses");
            throw;
        }
    }
}
