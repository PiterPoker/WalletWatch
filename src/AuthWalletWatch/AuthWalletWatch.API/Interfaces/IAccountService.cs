using AuthWalletWatch.API.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace AuthWalletWatch.API.Interfaces;

public interface IAccountService
{
    Task<AccessTokenDto> LoginAsync(LoginDto model);
    Task<IdentityResult> RegisterAsync(RegisterDto model);
    Task<AccessTokenDto> RefreshTokenAsync(RefreshTokenRequestDto refreshToken);
    Task<IdentityResult> ChangePasswordAsync(string userId, ChangePasswordDto model);
}