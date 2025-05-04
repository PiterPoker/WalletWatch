using AuthWalletWatch.Infrastructure.Models;
using System.IdentityModel.Tokens.Jwt;

namespace AuthWalletWatch.API.Interfaces;

public interface ITokenService
{
    JwtSecurityToken GenerateAccessToken(ApplicationUser user);
    ApplicationUserToken GenerateRefreshToken(ApplicationUser user); 
    Task<ApplicationUserToken> GetRefreshToken(string token);
}
