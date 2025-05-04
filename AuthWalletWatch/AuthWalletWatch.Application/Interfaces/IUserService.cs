using AuthWalletWatch.Application.DTOs;
using Microsoft.AspNetCore.Identity;

namespace AuthWalletWatch.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<ReadUserDto>> GetAllUsersAsync();
    Task<ReadUserDto> GetUserByIdAsync(string id);
    Task<IdentityResult> CreateUserProfileAsync(string userId, WriteProfileDto profileDto);
    Task<ReadProfileDto> GetUserProfileAsync(string userId);
    Task<IdentityResult> UpdateUserAsync(string id, WriteUserDto userDto);
    Task<IdentityResult> DeleteUserAsync(string id);
    Task<IdentityResult> AssignRolesToUserAsync(string userId, List<string> roles);
    Task<IdentityResult> AddClaimToUserAsync(string userId, WriteUserClaimDto claimDto);
}