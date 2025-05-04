using Expenses.Application.Interfaces.Services;
using MassTransit;
using Wallet.Contracts.Wallet;

namespace Expenses.API.Consumers.WalletConsumers
{
    public class WalletDeletedConsumer: IConsumer<WalletDeleted>
    {
        private readonly IWalletService _walletService;
        private readonly ILogger<WalletDeletedConsumer> _logger;

        public WalletDeletedConsumer(IWalletService walletService, ILogger<WalletDeletedConsumer> logger)
        {
            _walletService = walletService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<WalletDeleted> context)
        {
            _logger.LogInformation($"WalletDeletedConsumer Consume: {context.Message.ToString()}");

            await _walletService.DeleteWalletAsync(context.Message.Id);
        }
    }
}
