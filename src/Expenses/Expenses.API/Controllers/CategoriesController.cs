using Expenses.API.Exceptions;
using Expenses.Application.DTOs.Category;
using Expenses.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Expenses.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[SwaggerTag("Operations related to categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
    {
        _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CategoryDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [SwaggerOperation(Summary = "Creates a new category")]
    public async Task<ActionResult<CategoryDto>> CreateCategory([SwaggerRequestBody(Description = "Data for creating a new category")] CreateCategoryDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _logger.LogInformation("Attempting to create a new category with name: {CategoryName}", dto.Name);

            var category = await _categoryService.CreateCategoryAsync(dto);
            if (category is null)
            {
                _logger.LogWarning("Failed to create category with name: {CategoryName}", dto.Name);
                return BadRequest();
            }

            _logger.LogInformation("Category created successfully with ID: {CategoryId}", category.Id);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while creating category with name: {CategoryName}", dto.Name);
            throw;
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CategoryDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [SwaggerOperation(Summary = "Retrieves a category by ID")]
    public async Task<ActionResult<CategoryDto>> GetCategoryById([SwaggerParameter(Description = "The unique identifier of the category")] Guid id)
    {
        try
        {
            _logger.LogInformation("Attempting to retrieve category with ID: {CategoryId}", id);

            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category is null)
            {
                _logger.LogWarning("Category with ID: {CategoryId} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Category retrieved successfully with ID: {CategoryId}", id);
            return Ok(category);
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving category with ID: {CategoryId}", id);
            throw;
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), (int)HttpStatusCode.OK)]
    [SwaggerOperation(Summary = "Retrieves all categories")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories()
    {
        try
        {
            _logger.LogInformation("Attempting to retrieve all categories");

            var categories = await _categoryService.GetAllCategoriesAsync();

            _logger.LogInformation("All categories retrieved successfully");
            return Ok(categories);
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving all categories");
            throw;
        }
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [SwaggerOperation(Summary = "Updates an existing category by ID")]
    public async Task<IActionResult> UpdateCategory(
        [SwaggerParameter(Description = "The unique identifier of the category to update")] Guid id,
        [SwaggerRequestBody(Description = "Data for updating the category")] UpdateCategoryDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _logger.LogInformation("Attempting to update category with ID: {CategoryId}", id);

            await _categoryService.UpdateCategoryAsync(id, dto);

            _logger.LogInformation("Category updated successfully with ID: {CategoryId}", id);
            return NoContent();
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while updating category with ID: {CategoryId}", id);
            throw;
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [SwaggerOperation(Summary = "Deletes a category by ID")]
    public async Task<IActionResult> DeleteCategory([SwaggerParameter(Description = "The unique identifier of the category to delete")] Guid id)
    {
        try
        {
            _logger.LogInformation("Attempting to delete category with ID: {CategoryId}", id);

            await _categoryService.DeleteCategoryAsync(id);

            _logger.LogInformation("Category deleted successfully with ID: {CategoryId}", id);
            return NoContent();
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while deleting category with ID: {CategoryId}", id);
            throw;
        }
    }
}