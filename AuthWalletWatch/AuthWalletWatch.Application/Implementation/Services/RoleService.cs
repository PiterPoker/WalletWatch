using AuthWalletWatch.Application.DTOs;
using AuthWalletWatch.Application.Interfaces;
using AuthWalletWatch.Infrastructure.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthWalletWatch.Application.Implementation.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IMapper _mapper;

    public RoleService(RoleManager<ApplicationRole> roleManager
        , IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReadRoleDto>> GetAllRolesAsync()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return _mapper.Map<IEnumerable<ReadRoleDto>>(roles);
    }

    public async Task<ReadRoleDto?> GetRoleByIdAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return null;
        }
        return _mapper.Map<ReadRoleDto>(role);
    }

    public async Task<IdentityResult> CreateRoleAsync(WriteRoleDto roleDto)
    {
        var role = _mapper.Map<ApplicationRole>(roleDto);
        return await _roleManager.CreateAsync(role);
    }

    public async Task<IdentityResult> UpdateRoleAsync(string id, WriteRoleDto roleDto)
    {
        var roleToUpdate = await _roleManager.FindByIdAsync(id);
        if (roleToUpdate == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = $"Role with id '{id}' not found." });
        }
        roleToUpdate.Name = roleDto.Name;
        return await _roleManager.UpdateAsync(roleToUpdate);
    }

    public async Task<IdentityResult> DeleteRoleAsync(string id)
    {
        var roleToDelete = await _roleManager.FindByIdAsync(id);
        if (roleToDelete == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = $"Role with id '{id}' not found." });
        }
        return await _roleManager.DeleteAsync(roleToDelete);
    }

    public async Task<IdentityResult> AddClaimToRoleAsync(string roleId, WriteRoleClaimDto claimDto)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = $"Role with id '{roleId}' not found." });
        }

        var result = await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(claimDto.ClaimType, claimDto.ClaimValue));
        return result;
    }
}
