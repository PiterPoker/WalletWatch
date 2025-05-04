using AuthWalletWatch.Application.DTOs;
using AuthWalletWatch.Application.Interfaces;
using AuthWalletWatch.Infrastructure.EF;
using AuthWalletWatch.Infrastructure.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthWalletWatch.Application.Implementation.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly AuthWalletWatchDBContext _dbContext;
    private readonly IMapper _mapper;

    public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, AuthWalletWatchDBContext dbContext, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReadUserDto>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.Include(u => u.Profile).ToListAsync();
        return _mapper.Map<IEnumerable<ReadUserDto>>(users);
    }

    public async Task<ReadUserDto> GetUserByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        return _mapper.Map<ReadUserDto>(user);
    }

    public async Task<IdentityResult> CreateUserProfileAsync(string userId, WriteProfileDto profileDto)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }

        var profile = _mapper.Map<ApplicationProfile>(profileDto);
        profile.UserId = Guid.Parse(userId);
        profile.User = user;

        _dbContext.Profiles.Add(profile);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0 ? IdentityResult.Success : IdentityResult.Failed(new IdentityError { Description = "Failed to create profile" });
    }

    public async Task<ReadProfileDto> GetUserProfileAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return null; // Or throw an exception
        }

        var profile = await _dbContext.Profiles.FirstOrDefaultAsync(p => p.UserId == Guid.Parse(userId));
        return _mapper.Map<ReadProfileDto>(profile);
    }

    public async Task<IdentityResult> UpdateUserAsync(string id, WriteUserDto userDto)
    {
        var existingUser = await _userManager.FindByIdAsync(id);
        if (existingUser == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }

        existingUser.UserName = userDto.UserName;
        existingUser.Email = userDto.Email;

        return await _userManager.UpdateAsync(existingUser);
    }

    public async Task<IdentityResult> DeleteUserAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }

        return await _userManager.DeleteAsync(user);
    }

    public async Task<IdentityResult> AssignRolesToUserAsync(string userId, List<string> roles)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }

        foreach (var roleName in roles)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Role '{roleName}' does not exist" });
            }
        }

        return await _userManager.AddToRolesAsync(user, roles);
    }

    public async Task<IdentityResult> AddClaimToUserAsync(string userId, WriteUserClaimDto claimDto)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = $"User with id '{userId}' not found." });
        }

        var result = await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim(claimDto.ClaimType, claimDto.ClaimValue));
        return result;
    }
}
