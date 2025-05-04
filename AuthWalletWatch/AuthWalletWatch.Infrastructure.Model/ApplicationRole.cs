using Microsoft.AspNetCore.Identity;

namespace AuthWalletWatch.Infrastructure.Models;

public class ApplicationRole : IdentityRole<Guid>
{
    public virtual List<ApplicationRoleClaim> RoleClaims { get; set; }
    public virtual List<ApplicationUser> Users { get; set; }

    public string? Description { get; private set; }

    public ApplicationRole()
    {
        Id = Guid.NewGuid();
    }

    public ApplicationRole(string name, string? description = null) : this()
    {
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name)); 
        Description = description;
    }

    public bool TryAddRoleClaim(ApplicationRoleClaim roleClaim)
    {
        if (roleClaim is ApplicationRoleClaim roleClaimEntity)
        {
            RoleClaims.Add(roleClaimEntity);
            return true;
        }
        return false;
    }

    public bool TryAddUserRole(ApplicationUser user)
    {
        if (user is ApplicationUser userEntity && !Users.Contains(userEntity))
        {
            Users.Add(userEntity);
            return true;
        }
        return false;
    }

    public bool TryChangeDescription(string? description)
    {
        Description = description;
        return true;
    }

    public bool TryChangeName(string? name)
    {
        if (name is null)
            return false;
        Name = name;
        return true;
    }

    public bool TryRemoveRoleClaim(ApplicationRoleClaim roleClaim)
    {
        if (roleClaim is ApplicationRoleClaim roleClaimEntity && RoleClaims.Contains(roleClaimEntity))
        {
            RoleClaims.Remove(roleClaimEntity);
            return true;
        }
        return false;
    }

    public bool TryRemoveUserRole(ApplicationUser user)
    {
        if (user is ApplicationUser userEntity && Users.Contains(userEntity))
        {
            Users.Remove(userEntity);
            return true;
        }
        return false;
    }
}