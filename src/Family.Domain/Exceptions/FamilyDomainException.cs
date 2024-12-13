using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family.Domain.Exceptions
{
    public class FamilyDomainException : Exception
    {
        public FamilyDomainException()
        {
        }

        public FamilyDomainException(string? message) : base(message)
        {
        }

        public FamilyDomainException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
