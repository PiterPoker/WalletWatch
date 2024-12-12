using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Domain.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class WalletDomainException : Exception
    {
        public WalletDomainException()
        { }

        public WalletDomainException(string message)
            : base(message)
        { }

        public WalletDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
