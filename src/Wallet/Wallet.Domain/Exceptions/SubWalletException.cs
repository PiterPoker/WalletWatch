namespace Wallet.Domain.Exceptions;

public class SubWalletException : Exception
{
    public SubWalletException()
    { }

    public SubWalletException(string message)
        : base(message)
    { }

    public SubWalletException(string message, Exception innerException)
        : base(message, innerException)
    { }
}