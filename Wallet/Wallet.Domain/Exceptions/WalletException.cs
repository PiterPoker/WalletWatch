namespace Wallet.Domain.Exceptions;

public class WalletException : Exception
{
    public WalletException()
    { }

    public WalletException(string message)
        : base(message)
    { }

    public WalletException(string message, Exception innerException)
        : base(message, innerException)
    { }
}