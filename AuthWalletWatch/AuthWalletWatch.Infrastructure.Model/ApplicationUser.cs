using Microsoft.AspNetCore.Identity;

namespace AuthWalletWatch.Infrastructure.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    public virtual List<ApplicationUserClaim> Claims { get; set; }
    public virtual List<ApplicationUserLogin> Logins { get; set; }
    public virtual List<ApplicationUserToken> Tokens { get; set; }
    public virtual List<ApplicationRole> Roles { get; set; }
    public ApplicationUser()
    {
        Id = Guid.NewGuid();
    }

    public ApplicationUser(string email) : this()
    {
        Email = !string.IsNullOrWhiteSpace(email) ? email : throw new ArgumentNullException(nameof(email));
        UserName = email;
    }
    public virtual Guid ProfileId { get; set; }
    public virtual ApplicationProfile Profile { get; set; }

    public ApplicationUserLogin? FindUserLogin(string loginProvider, string providerKey)
    {
        return Logins.FirstOrDefault(l => l.LoginProvider == loginProvider && l.ProviderKey == providerKey);
    }

    public ApplicationRole? FindUserRole(Guid roleId)
    {
        return Roles.FirstOrDefault(r => r.Id == roleId);
    }

    public ApplicationUserToken? FindUserToken(string loginProvider, string name)
    {
        return Tokens.FirstOrDefault(t => t.LoginProvider == loginProvider && t.Name == name);
    }

    public IReadOnlyCollection<ApplicationUserLogin> GetUserLoginsByUserId(Guid userId)
    {
        return Logins.Where(l => l.UserId == userId).ToList().AsReadOnly();
    }

    public IReadOnlyCollection<ApplicationRole> GetUserRolesByUserId(Guid userId)
    {
        return Roles.Where(r => r.Id == userId).ToList().AsReadOnly();
    }

    public IReadOnlyCollection<ApplicationUserToken> GetUserTokensByUserId(Guid userId)
    {
        return Tokens.Where(t => t.UserId == userId).ToList().AsReadOnly();
    }

    public bool TryAddUserClaim(ApplicationUserClaim claim)
    {
        if (claim is ApplicationUserClaim claimEntity && !Claims.Contains(claimEntity))
        {
            Claims.Add(claimEntity);
            return true;
        }
        return false;
    }

    public bool TryAddUserLogin(ApplicationUserLogin login)
    {
        if (login is ApplicationUserLogin loginEntity && !Logins.Contains(loginEntity))
        {
            Logins.Add(loginEntity);
            return true;
        }
        return false;
    }

    public bool TryAddUserRole(ApplicationRole role)
    {
        if (role is ApplicationRole roleEntity && !Roles.Contains(roleEntity))
        {
            Roles.Add(roleEntity);
            return true;
        }
        return false;
    }

    public bool TryAddUserToken(ApplicationUserToken token)
    {
        if (token is ApplicationUserToken tokenEntity && !Tokens.Contains(tokenEntity))
        {
            Tokens.Add(tokenEntity);
            return true;
        }
        return false;
    }

    public bool TryChangeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        Email = email;
        return true;
    }

    public bool TryChangeLockoutEnabled(bool lockoutEnabled)
    {
        LockoutEnabled = lockoutEnabled;
        return true;
    }

    public bool TryChangePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber)) return false;
        PhoneNumber = phoneNumber;
        return true;
    }

    public bool TryChangeTwoFactorEnabled(bool twoFactorEnabled)
    {
        TwoFactorEnabled = twoFactorEnabled;
        return true;
    }

    public bool TryChangeUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName)) return false;
        UserName = userName;
        return true;
    }

    public bool TryConfirmEmail()
    {
        EmailConfirmed = true;
        return true;
    }

    public bool TryConfirmPhoneNumber()
    {
        PhoneNumberConfirmed = true;
        return true;
    }

    public bool TryRemoveAllUserClaims()
    {
        Claims.Clear();
        return true;
    }

    public bool TryRemoveAllUserLogins()
    {
        Logins.Clear();
        return true;
    }

    public bool TryRemoveAllUserRoles()
    {
        Roles.Clear();
        return true;
    }

    public bool TryRemoveAllUserTokens()
    {
        Tokens.Clear();
        return true;
    }

    public bool TryRemoveUserClaim(ApplicationUserClaim claim)
    {
        if (claim is ApplicationUserClaim claimEntity && Claims.Contains(claimEntity))
        {
            Claims.Remove(claimEntity);
            return true;
        }
        return false;
    }

    public bool TryRemoveUserLogin(ApplicationUserLogin login)
    {
        if (login is ApplicationUserLogin loginEntity && Logins.Contains(loginEntity))
        {
            Logins.Remove(loginEntity);
            return true;
        }
        return false;
    }

    public bool TryRemoveUserLoginByProvider(string loginProvider, string providerKey)
    {
        var login = Logins.FirstOrDefault(l => l.LoginProvider == loginProvider && l.ProviderKey == providerKey);
        if (login is not null)
        {
            Logins.Remove(login);
            return true;
        }
        return false;
    }

    public bool TryRemoveUserRole(ApplicationRole role)
    {
        if (role is ApplicationRole roleEntity && Roles.Contains(roleEntity))
        {
            Roles.Remove(roleEntity);
            return true;
        }
        return false;
    }

    public bool TryRemoveUserRoleByRoleId(Guid roleId)
    {
        var role = Roles.FirstOrDefault(r => r.Id == roleId);
        if (role is not null)
        {
            Roles.Remove(role);
            return true;
        }
        return false;
    }

    public bool TryRemoveUserToken(ApplicationUserToken token)
    {
        if (token is ApplicationUserToken tokenEntity && Tokens.Contains(tokenEntity))
        {
            Tokens.Remove(tokenEntity);
            return true;
        }
        return false;
    }

    public bool TryRemoveUserTokenByName(string loginProvider, string name)
    {
        var token = Tokens.FirstOrDefault(t => t.LoginProvider == loginProvider && t.Name == name);
        if (token is not null)
        {
            Tokens.Remove(token);
            return true;
        }
        return false;
    }
}