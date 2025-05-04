using AuthWalletWatch.API.Interfaces;
using AuthWalletWatch.API.Models;
using AuthWalletWatch.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthWalletWatch.API.Implementation.Services;

public class JwtTokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<ApplicationUser> _userManager;

    public JwtTokenService(IOptions<JwtSettings> jwtSettings, UserManager<ApplicationUser> userManager)
    {
        _jwtSettings = jwtSettings.Value;
        _userManager = userManager;
    }

    public JwtSecurityToken GenerateAccessToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };

        //var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var notBefore = DateTime.Now;
        var expires = notBefore.AddSeconds(_jwtSettings.AccessTokenExpirationSeconds);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            notBefore: notBefore,
            expires: expires,
            signingCredentials: creds
        );

        return token;
    }

    public ApplicationUserToken GenerateRefreshToken(ApplicationUser user)
    {
        // Более сложная реализация может включать claims и время истечения

        var refreshToken = new ApplicationUserToken
        {
            Value = GenerateUniqueToken(),
            ExpiresAt = _jwtSettings.RefreshTokenExpirationDays,
            Create = DateTime.Now,
            UserId = user.Id,
            LoginProvider = _jwtSettings.Audience,
            Name = "RefreshToken"
        };

        return refreshToken;
    }

    public async Task<ApplicationUserToken> GetRefreshToken(string token)
    {
        return await _userManager.Users
            .SelectMany(u => u.Tokens)
            .SingleAsync(rt => rt.Value == token);
    }

    private string GenerateUniqueToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
