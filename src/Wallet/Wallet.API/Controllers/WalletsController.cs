using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Wallet.API.Applications.Exceptions;
using Wallet.API.Models.Base;
using Wallet.API.Models.WalletOfFamily;
using Wallet.API.Services.Abstractions;
using WalletOfFamily = Wallet.API.Models.WalletOfFamily;

namespace Wallet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletsController : ControllerBase
    {
        private readonly ILogger<WalletsController> _logger;
        private readonly IWalletService _walletService;

        public WalletsController(IWalletService walletService,
            ILogger<WalletsController> logger)
        {
            _walletService = walletService ?? throw new WalletAPIException($"Property {nameof(walletService)} cannot be null");
            _logger = logger ?? throw new WalletAPIException($"Property {nameof(logger)} cannot be null");
        }


        /// <summary>
        /// �������� ��� ��������.
        /// </summary>
        /// <returns>������ ���� ���������</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "�������� ��� ��������", Description = "���������� ������ ���� ��������� �� ���� ������.")]
        [SwaggerResponse(200, "���������� ������ ���� ���������", typeof(PaginatedItems<WalletOfFamily.WalletReadModel>))]
        public async Task<ActionResult<PaginatedItems<WalletOfFamily.WalletReadModel>>> GetWallets(CancellationToken cancellation, [SwaggerParameter(Description = "��������")] int pageIndex = 1, [SwaggerParameter(Description = "���������� ������� �� ��������")] int pageSize = 10)
        {
            try
            {
                return await _walletService.GetWallets(pageIndex, pageSize, cancellation);
            }
            catch (WalletAPIException ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }


        /// <summary>
        /// �������� ������ �� Id.
        /// </summary>
        /// <returns>������ � ��������� Id</returns>
        [HttpGet("{id:guid}")]
        [SwaggerOperation(Summary = "�������� ������ �� Id", Description = "������ � ��������� Id �� ���� ������.")]
        [SwaggerResponse(200, "������ � ��������� Id �� ���� ������.", typeof(WalletOfFamily.WalletReadModel))]
        public async Task<ActionResult<WalletOfFamily.WalletReadModel>> GetWallet([SwaggerParameter(Description = "Id ��������", Required = true)] Guid id, CancellationToken cancellation)
        {
            try
            {
                return await _walletService.GetWallet(id, cancellation);
            }
            catch (WalletAPIException ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }


        /// <summary>
        /// ������� ������
        /// </summary>
        /// <returns>��������� ������.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "������� ������", Description = "���������� ��������� ������.")]
        [SwaggerResponse(200, "���������� ��������� ������", typeof(WalletOfFamily.WalletReadModel))]
        public async Task<ActionResult<WalletOfFamily.WalletReadModel>> Create([SwaggerRequestBody("���������� � ����� �������� �����")] WalletWriteModel wallet, CancellationToken cancellation)
        {
            try
            {
                var resultWallet = await _walletService.CreateWallet(wallet, cancellation);

                return Ok(resultWallet);
            }
            catch (WalletAPIException ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }


        /// <summary>
        /// ��������� ���������� �� ��������
        /// </summary>
        /// <returns>���������� ����������� ���������� �� ��������.</returns>
        [HttpPut("{id:guid}")]
        [SwaggerOperation(Summary = "�������� ���������� �� ��������", Description = "���������� ����������� ���������� �� ��������.")]
        [SwaggerResponse(200, "���������� ����������� ���������� �� ��������", typeof(WalletOfFamily.WalletReadModel))]
        public async Task<ActionResult<WalletOfFamily.WalletReadModel>> Update([SwaggerParameter(Description = "Id ��������", Required = true)] Guid id, [SwaggerRequestBody(Description = "������ ��� ����������", Required = true)] WalletWriteModel updateWallet, CancellationToken cancellation)
        {
            try
            {
                return await _walletService.UpdateWallet(id, updateWallet, cancellation);
            }
            catch (WalletAPIException ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }


        /// <summary>
        /// ������� ������
        /// </summary>
        /// <param name="id">Id ��������</param>
        /// <param name="cancellation">����� ������</param>
        [HttpDelete("{id:guid}")]
        [SwaggerOperation(Summary = "������� ������", Description = "���������� ������ 204 ��� �������� ����������.")]
        [SwaggerResponse(204, "���������� ��� �������� ����������")]
        [SwaggerResponse(400, "������ ��� �������� ��������", typeof(string))]
        public async Task<IActionResult> Delete([SwaggerParameter(Description = "Id ��������", Required = true)] Guid id, CancellationToken cancellation)
        {
            try
            {
                await _walletService.DeleteWallet(id, cancellation);
                return NoContent();
            }
            catch (WalletAPIException ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }

        /// <summary> 
        /// ��������� ��������� ����� ����� ����� �������� ��������� � ������������. 
        /// </summary> 
        /// <param name="transferFunds">������ � ������� ��� ��������.</param> 
        /// <param name="cancellation">����� ������ ��������.</param> 
        /// <returns>��������� �������� �������� �������.</returns> 
        /// <response code="200">�������� ������� �������.</response> 
        /// <response code="400">�������� ������ ��� ���������� ������ (��������, ������������ �����).</response>
        /// <response code="500">���������� ������ �������.</response> 
        [HttpPost("TransferFunds")]
        [SwaggerOperation(Summary = "��������� ��������� ����� ����� ����� �������� ��������� � ������������.")]
        [SwaggerResponse(200, "Funds transferred successfully.")]
        [SwaggerResponse(400, "Invalid request or logic error.")]
        public async Task<IActionResult> TransferFunds([SwaggerRequestBody(Description = "������ ��� �������� ������� ����� ����������", Required = true)] WalletTransferFundsWriteModel transferFunds, CancellationToken cancellation)
        {
            _logger.LogInformation("Initiating transfer of {Amount} from wallet {MainWalletId} to wallet {request.SubWalletId}.", transferFunds.Amount, transferFunds.FromWalletId, transferFunds.ToWalletId);
            try
            {
                await _walletService.TransferFundsBetweenWalletAndSubWallet(transferFunds, cancellation);
                _logger.LogInformation("Successfully transferred {Amount} from wallet {MainWalletId} to wallet {SubWalletId}.", transferFunds.Amount, transferFunds.FromWalletId, transferFunds.ToWalletId);
                return Ok(new { Message = "Funds transferred successfully." });
            }
            catch (WalletAPIException ex)
            {
                _logger.LogWarning(ex, "WalletAPIException: {Message}", ex.Message);
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while transferring funds.");
                return StatusCode(500, new { Error = "An unexpected error occurred." });
            }
        }
    }
}
