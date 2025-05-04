using Microsoft.AspNetCore.Identity;

namespace AuthWalletWatch.Infrastructure.Models;

public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
{
    public virtual ApplicationRole Role { get; set; }

    public bool TryChangeClaimType(string? claimType)
    {
        if (string.IsNullOrWhiteSpace(claimType))
        {
            return false;
        }

        ClaimType = claimType;
        return true;
    }

    public bool TryChangeClaimValue(string? claimValue)
    {
        if (string.IsNullOrWhiteSpace(claimValue))
        {
            return false;
        }

        ClaimValue = claimValue;
        return true;
    }
}