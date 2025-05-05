using Expenses.API.Exceptions;
using Expenses.Application.DTOs.Author;
using Expenses.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Expenses.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[SwaggerTag("Operations related to authors")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly ILogger<AuthorsController> _logger;

    public AuthorsController(IAuthorService authorService, ILogger<AuthorsController> logger)
    {
        _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost]
    [ProducesResponseType(typeof(AuthorDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [SwaggerOperation(Summary = "Creates a new author")]
    public async Task<ActionResult<AuthorDto>> CreateAuthor([SwaggerRequestBody(Description = "Data for creating a new author")] CreateAuthorDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _logger.LogInformation("Attempting to create a new author with name: {AuthorName}", dto.Name);

            var author = await _authorService.CreateAuthorAsync(dto);
            if (author is null)
            {
                _logger.LogWarning("Failed to create author with name: {AuthorName}", dto.Name);
                return BadRequest();
            }
            
            _logger.LogInformation("Author created successfully with ID: {AuthorId}", author.Id);
            return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while creating author with name: {AuthorName}", dto.Name);
            throw;
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AuthorDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [SwaggerOperation(Summary = "Retrieves an author by ID")]
    public async Task<ActionResult<AuthorDto>> GetAuthorById([SwaggerParameter(Description = "The unique identifier of the author")] Guid id)
    {
        try
        {
            _logger.LogInformation("Attempting to retrieve author with ID: {AuthorId}", id);

            var author = await _authorService.GetAuthorByIdAsync(id);

            if (author is null)
            {
                _logger.LogWarning("Author with ID: {AuthorId} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Author retrieved successfully with ID: {AuthorId}", id);
            return Ok(author);
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving author with ID: {AuthorId}", id);
            throw;
        }
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [SwaggerOperation(Summary = "Updates an existing author by ID")]
    public async Task<IActionResult> UpdateAuthor(
    [SwaggerParameter(Description = "The unique identifier of the author to update")] Guid id,
    [SwaggerRequestBody(Description = "Data for updating the author")] UpdateAuthorDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _logger.LogInformation("Attempting to update author with ID: {AuthorId}", id);

            await _authorService.UpdateAuthorAsync(id, dto);

            _logger.LogInformation("Author updated successfully with ID: {AuthorId}", id);
            return NoContent();
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while updating author with ID: {AuthorId}", id);
            throw;
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [SwaggerOperation(Summary = "Deletes an author by ID")]
    public async Task<IActionResult> DeleteAuthor([SwaggerParameter(Description = "The unique identifier of the author to delete")] Guid id)
    {
        try
        {
            _logger.LogInformation("Attempting to delete author with ID: {AuthorId}", id);

            await _authorService.DeleteAuthorAsync(id);

            _logger.LogInformation("Author deleted successfully with ID: {AuthorId}", id);
            return NoContent();
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while deleting author with ID: {AuthorId}", id);
            throw;
        }
    }
}
