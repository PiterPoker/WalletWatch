using Expenses.Application.DTOs.Wallet;
using Expenses.Application.Interfaces.Services;
using MassTransit;
using Wallet.Contracts.Wallet;

namespace Expenses.API.Consumers.WalletConsumers
{
    public class WalletUpdatedConsumer :IConsumer<WalletUpdated>
    {
        private readonly IWalletService _walletService;
        private readonly ILogger<WalletUpdatedConsumer> _logger;


        public WalletUpdatedConsumer(IWalletService walletService, ILogger<WalletUpdatedConsumer> logger)
        {
            _walletService = walletService;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<WalletUpdated> context)
        {
            _logger.LogInformation($"WalletUpdatedConsumer Consume: {context.Message.ToString()}");

            var walletUpdateDto = new UpdateWalletDto { Name = context.Message.WalletName };
            await _walletService.UpdateWalletAsync(context.Message.Id, walletUpdateDto);
        }
    }
}
