using Expenses.API.Exceptions;
using Expenses.Application.DTOs.Wallet;
using Expenses.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Expenses.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[SwaggerTag("Operations related to wallets")]
public class WalletsController : ControllerBase
{
    private readonly IWalletService _walletService;
    private readonly ILogger<WalletsController> _logger;

    public WalletsController(IWalletService walletService, ILogger<WalletsController> logger)
    {
        _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost]
    [ProducesResponseType(typeof(WalletDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [SwaggerOperation(Summary = "Creates a new wallet")]
    public async Task<ActionResult<WalletDto>> CreateWallet([SwaggerRequestBody(Description = "Data for creating a new wallet")] CreateWalletDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _logger.LogInformation("Attempting to create a new wallet with name: {WalletName}", dto.Name);

            var wallet = await _walletService.CreateWalletAsync(dto);
            if (wallet is null)
            {
                _logger.LogWarning("Failed to create wallet with name: {WalletName}", dto.Name);
                return BadRequest();
            }

            _logger.LogInformation("Wallet created successfully with ID: {WalletId}", wallet.Id);
            return CreatedAtAction(nameof(GetWalletById), new { id = wallet.Id }, wallet);
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while creating wallet with name: {WalletName}", dto.Name);
            throw;
        }
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(WalletDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [SwaggerOperation(Summary = "Retrieves a wallet by ID")]
    public async Task<ActionResult<WalletDto>> GetWalletById([SwaggerParameter(Description = "The unique identifier of the wallet")] Guid id)
    {
        try
        {
            _logger.LogInformation("Attempting to retrieve wallet with ID: {WalletId}", id);

            var wallet = await _walletService.GetWalletByIdAsync(id);

            if (wallet is null)
            {
                _logger.LogWarning("Wallet with ID: {WalletId} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Wallet retrieved successfully with ID: {WalletId}", id);
            return Ok(wallet);
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving wallet with ID: {WalletId}", id);
            throw;
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WalletDto>), (int)HttpStatusCode.OK)]
    [SwaggerOperation(Summary = "Retrieves all wallets")]
    public async Task<ActionResult<IEnumerable<WalletDto>>> GetAllWallets()
    {
        try
        {
            _logger.LogInformation("Attempting to retrieve all wallets");

            var wallets = await _walletService.GetAllWalletsAsync();

            _logger.LogInformation("All wallets retrieved successfully");
            return Ok(wallets);
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving all wallets");
            throw;
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [SwaggerOperation(Summary = "Updates an existing wallet by ID")]
    public async Task<IActionResult> UpdateWallet(
        [SwaggerParameter(Description = "The unique identifier of the wallet to update")] Guid id,
        [SwaggerRequestBody(Description = "Data for updating the wallet")] UpdateWalletDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _logger.LogInformation("Attempting to update wallet with ID: {WalletId}", id);

            await _walletService.UpdateWalletAsync(id, dto);

            _logger.LogInformation("Wallet updated successfully with ID: {WalletId}", id);
            return NoContent();
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while updating wallet with ID: {WalletId}", id);
            throw;
        }
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [SwaggerOperation(Summary = "Deletes a wallet by ID")]
    public async Task<IActionResult> DeleteWallet([SwaggerParameter(Description = "The unique identifier of the wallet to delete")] Guid id)
    {
        try
        {
            _logger.LogInformation("Attempting to delete wallet with ID: {WalletId}", id);

            await _walletService.DeleteWalletAsync(id);

            _logger.LogInformation("Wallet deleted successfully with ID: {WalletId}", id);
            return NoContent();
        }
        catch (ExpenseAPIException ex)
        {
            _logger.LogError(ex, "An error occurred while deleting wallet with ID: {WalletId}", id);
            throw;
        }
    }
}