using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Domain.SeedWork;

namespace Wallet.Infrastructure.Repositories
{
    public class FamilyRepository<TEntity, TId> : Repository<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : struct
    {
        public FamilyRepository(WalletDBContext context) : base(context)
        {
        }
    }
}
