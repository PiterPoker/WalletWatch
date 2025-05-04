using AuthWalletWatch.API.Interfaces;
using AuthWalletWatch.API.Models.DTOs;
using AuthWalletWatch.Infrastructure.Models;
using AuthWalletWatch.Infrastructure.NoSQL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace AuthWalletWatch.API.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly ITokenRepository _tokenRepository;

    public AccountService(UserManager<ApplicationUser> userManager
        , ITokenService tokenService
        , ITokenRepository tokenRepository)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _tokenRepository = tokenRepository ?? throw new ArgumentNullException(nameof(tokenRepository));
    }

    public async Task<AccessTokenDto> LoginAsync(LoginDto model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);

        if (user is null && !await _userManager.CheckPasswordAsync(user, model.Password))
            throw new Exception("Incorrect credentials");

        var tokenCash = _tokenRepository.Get(user.Id);
        if (tokenCash is not null)
        {
            return CreateCashAccessTokenDto(tokenCash);
        }

        var token = _tokenService.GenerateAccessToken(user);

        var refreshToken = _tokenService.GenerateRefreshToken(user);

        //user.TryAddUserToken(refreshToken);

        await _userManager.SetAuthenticationTokenAsync(user, refreshToken.LoginProvider, refreshToken.Name, refreshToken.Value);
        var accessTokenDto = CreateAccessTokenDto(token, refreshToken);
        tokenCash = CreateCashAccessToken(user, token, accessTokenDto);
        _tokenRepository.Create(tokenCash);

        return accessTokenDto;
    }

    private static AccessTokenDto CreateCashAccessTokenDto(ApplicationUserToken tokenCash)
        => new AccessTokenDto()
    {
        AccessToken = tokenCash.Value,
        ExpiresIn = (long)(tokenCash.Create - DateTime.Now).TotalSeconds,
        TokenType = tokenCash.LoginProvider
    };

    public async Task<IdentityResult> RegisterAsync(RegisterDto model)
    {
        ApplicationUser user = new()
        {
            Email = model.Email,
            UserName = model.Username
        };

        return await _userManager.CreateAsync(user, model.Password);
    }

    public async Task<AccessTokenDto> RefreshTokenAsync(RefreshTokenRequestDto refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken.RefreshToken))
            throw new ArgumentNullException(nameof(refreshToken.RefreshToken));

        var storedRefreshToken = await _tokenService.GetRefreshToken(refreshToken.RefreshToken);

        if (storedRefreshToken is null || storedRefreshToken.ExpiresDate < DateTime.Now || storedRefreshToken?.User?.UserName == null)
            throw new Exception("Invalid token");

        var user = await _userManager.FindByNameAsync(storedRefreshToken.User.UserName);

        user.TryRemoveUserToken(storedRefreshToken);

        var newAccessToken = _tokenService.GenerateAccessToken(user);
        var newRefreshToken = _tokenService.GenerateRefreshToken(user);
        AccessTokenDto newAccessTokenDto = CreateAccessTokenDto(newAccessToken, newRefreshToken);
        ApplicationUserToken accessTokenCash = CreateCashAccessToken(user, newAccessToken, newAccessTokenDto);
        _tokenRepository.Create(accessTokenCash);
        user.TryAddUserToken(newRefreshToken);

        await _userManager.UpdateAsync(user);

        return newAccessTokenDto;
    }

    private static AccessTokenDto CreateAccessTokenDto(JwtSecurityToken newAccessToken, ApplicationUserToken newRefreshToken) 
        => new AccessTokenDto
    {
        TokenType = JwtBearerDefaults.AuthenticationScheme,
        AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
        ExpiresIn = (long)(newAccessToken.ValidTo - newAccessToken.ValidFrom).TotalSeconds,
        RefreshToken = newRefreshToken.Value
    };

    private static ApplicationUserToken CreateCashAccessToken(ApplicationUser user, JwtSecurityToken newAccessToken, AccessTokenDto newAccessTokenDto)
        => new ApplicationUserToken
    {
        LoginProvider = newAccessTokenDto.TokenType,
        Create = newAccessToken.ValidTo,
        Value = newAccessTokenDto.AccessToken,
        ExpiresAt = newAccessTokenDto.ExpiresIn,
        UserId = user.Id,
    };

    public async Task<IdentityResult> ChangePasswordAsync(string userId, ChangePasswordDto model)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            throw new ArgumentNullException(nameof(user));

        return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
    }
}
