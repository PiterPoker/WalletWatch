using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Domain.Models.WalletOfFamily;
using Wallet.Domain.Models.BaseEntity;

namespace Wallet.Infrastructure.Configurations
{
    internal class HeadMemberConfiguration
    : IEntityTypeConfiguration<HeadMember>
    {
        public void Configure(EntityTypeBuilder<HeadMember> builder)
        {
            builder.HasBaseType<FamilyMember>();
        }
    }
}
