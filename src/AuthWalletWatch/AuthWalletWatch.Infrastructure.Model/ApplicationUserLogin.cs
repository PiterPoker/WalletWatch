using Microsoft.AspNetCore.Identity;

namespace AuthWalletWatch.Infrastructure.Models;

public class ApplicationUserLogin : IdentityUserLogin<Guid>
{
    public virtual ApplicationUser User { get; set; }
    public bool TryChangeProviderDisplayName(string? providerDisplayName)
    {
        if (string.IsNullOrWhiteSpace(providerDisplayName))
        {
            return false;
        }

        ProviderDisplayName = providerDisplayName;
        return true;
    }
}
