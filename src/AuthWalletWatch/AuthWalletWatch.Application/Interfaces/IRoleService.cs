using AuthWalletWatch.Application.DTOs;
using Microsoft.AspNetCore.Identity;

namespace AuthWalletWatch.Application.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<ReadRoleDto>> GetAllRolesAsync();
    Task<ReadRoleDto?> GetRoleByIdAsync(string id);
    Task<IdentityResult> CreateRoleAsync(WriteRoleDto roleDto);
    Task<IdentityResult> UpdateRoleAsync(string id, WriteRoleDto roleDto);
    Task<IdentityResult> DeleteRoleAsync(string id);
    Task<IdentityResult> AddClaimToRoleAsync(string roleId, WriteRoleClaimDto claimDto);
}
