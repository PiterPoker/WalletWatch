namespace AuthWalletWatch.Infrastructure.EF.Exceptions;

internal class UnitOfWorkException : Exception
{
    public UnitOfWorkException()
    { }

    public UnitOfWorkException(string message)
        : base(message)
    { }

    public UnitOfWorkException(string message, Exception innerException)
        : base(message, innerException)
    { }
}