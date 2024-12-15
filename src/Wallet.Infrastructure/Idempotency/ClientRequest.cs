﻿namespace Wallet.Infrastructure.Idempotency
{
    public class ClientRequest
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime Time { get; set; }
    }
}
