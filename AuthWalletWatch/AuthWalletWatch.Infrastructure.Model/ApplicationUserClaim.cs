using Microsoft.AspNetCore.Identity;

namespace AuthWalletWatch.Infrastructure.Models;

public class ApplicationUserClaim : IdentityUserClaim<Guid>
{
    public virtual ApplicationUser User { get; set; }

    public bool IsValid() => !string.IsNullOrWhiteSpace(ClaimType) && !string.IsNullOrWhiteSpace(ClaimValue);

    public bool TryChangeClaimType(string claimType)
    {
        if (string.IsNullOrWhiteSpace(claimType))
        {
            return false;
        }

        ClaimType = claimType;
        return true;
    }

    public bool TryChangeClaimValue(string claimValue)
    {
        if (string.IsNullOrWhiteSpace(claimValue))
        {
            return false;
        }

        ClaimValue = claimValue;
        return true;
    }
}