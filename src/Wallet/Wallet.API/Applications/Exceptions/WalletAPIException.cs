namespace Wallet.API.Applications.Exceptions
{
    /// <summary>
    /// Исключения в Web-API (прикладной уровень)
    /// </summary>
    public class WalletAPIException : Exception
    {
        public WalletAPIException()
        { }

        public WalletAPIException(string message)
            : base(message)
        { }

        public WalletAPIException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
