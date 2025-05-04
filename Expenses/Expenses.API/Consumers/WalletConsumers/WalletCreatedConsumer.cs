using Expenses.Application.DTOs.Wallet;
using Expenses.Application.Interfaces.Services;
using MassTransit;
using Wallet.Contracts.Wallet;

namespace Expenses.API.Consumers.WalletConsumers
{
    public class WalletCreatedConsumer : IConsumer<WalletCreated>
    {
        private readonly IWalletService _walletService;
        private readonly ILogger<WalletCreatedConsumer> _logger;

        public WalletCreatedConsumer(IWalletService walletService, ILogger<WalletCreatedConsumer> logger)
        {
            _walletService = walletService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<WalletCreated> context)
        {
            _logger.LogInformation($"WalletCreatedConsumer Consume: {context.Message.ToString()}");
            
            var walletDto = new CreateWalletDto
            {
                Id = context.Message.Id,
                Name = context.Message.WalletName
            };

            await _walletService.CreateWalletAsync(walletDto);
        }
    }
}
