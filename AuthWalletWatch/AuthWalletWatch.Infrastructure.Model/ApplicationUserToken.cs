using Microsoft.AspNetCore.Identity;

namespace AuthWalletWatch.Infrastructure.Models;

public class ApplicationUserToken : IdentityUserToken<Guid>
{
    public virtual ApplicationUser User { get; set; }
    public long ExpiresAt { get; set; }
    public DateTime Create { get; set; }
    public bool TryChangeValue(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        Value = value;
        return true;
    }

    public DateTime ExpiresDate => Create.AddDays(ExpiresAt);
}
