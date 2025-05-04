using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.API.Models.Base
{
    public interface IReadModel<out TId> 
        where TId : struct
    {
        public TId Id { get; }
    }
}
