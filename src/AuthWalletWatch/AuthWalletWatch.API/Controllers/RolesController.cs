using AuthWalletWatch.Application.DTOs;
using AuthWalletWatch.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthWalletWatch.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    // GET: /api/Roles
    [HttpGet]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ReadRoleDto>>> GetRoles()
    {
        var roles = await _roleService.GetAllRolesAsync();
        return Ok(roles);
    }

    // GET: /api/Roles/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReadRoleDto>> GetRole(string id)
    {
        var role = await _roleService.GetRoleByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }
        return Ok(role);
    }

    // POST: /api/Roles
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReadRoleDto>> CreateRole([FromBody] WriteRoleDto roleDto)
    {
        if (ModelState.IsValid)
        {
            IdentityResult result = await _roleService.CreateRoleAsync(roleDto);
            if (result.Succeeded)
            {
                return Created();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return BadRequest(ModelState);
            }
        }
        return BadRequest(ModelState);
    }

    // PUT: /api/Roles/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRole(string id, [FromBody] WriteRoleDto roleDto)
    {
        if (!string.IsNullOrWhiteSpace(id))
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var result = await _roleService.UpdateRoleAsync(id, roleDto);
            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return BadRequest(ModelState);
            }
        }
        return BadRequest(ModelState);
    }

    // DELETE: /api/Roles/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var result = await _roleService.DeleteRoleAsync(id);
        if (result.Succeeded)
        {
            return NoContent();
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }
    }

    [HttpPost("{roleId}/claims")]
    public async Task<IActionResult> AddClaimToRole(string roleId, [FromBody] WriteRoleClaimDto claimDto)
    {
        if (claimDto == null)
        {
            return BadRequest("Claim data cannot be null.");
        }

        var result = await _roleService.AddClaimToRoleAsync(roleId, claimDto);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok("Claim successfully added to role.");
    }
}