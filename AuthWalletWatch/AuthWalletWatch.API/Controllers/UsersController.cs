using AuthWalletWatch.Application.DTOs;
using AuthWalletWatch.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthWalletWatch.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _accountService;

    public UsersController(IUserService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReadUserDto>>> GetUsers()
    {
        var users = await _accountService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReadUserDto>> GetUser(string id)
    {
        var user = await _accountService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost("{userId}/profile")]
    public async Task<ActionResult<IdentityResult>> CreateProfile(string userId, [FromBody] WriteProfileDto profileDto)
    {
        var result = await _accountService.CreateUserProfileAsync(userId, profileDto);
        if (result.Succeeded)
        {
            var profile = await _accountService.GetUserProfileAsync(userId);
            return CreatedAtAction(nameof(GetProfile), new { userId = userId }, profile);
        }
        return BadRequest(result.Errors);
    }

    [HttpGet("{userId}/profile")]
    public async Task<ActionResult<ReadProfileDto>> GetProfile(string userId)
    {
        var profile = await _accountService.GetUserProfileAsync(userId);
        if (profile == null)
        {
            return NotFound("Profile not found");
        }
        return Ok(profile);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] WriteUserDto userDto)
    {
        var result = await _accountService.UpdateUserAsync(id, userDto);
        if (result.Succeeded)
        {
            return NoContent();
        }
        return BadRequest(result.Errors);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var result = await _accountService.DeleteUserAsync(id);
        if (result.Succeeded)
        {
            return NoContent();
        }
        return BadRequest(result.Errors);
    }

    [HttpPost("{userId}/roles")]
    public async Task<IActionResult> AssignRolesToUser(string userId, [FromBody] List<string> roles)
    {
        var result = await _accountService.AssignRolesToUserAsync(userId, roles);
        if (result.Succeeded)
        {
            return Ok($"Roles '{string.Join(", ", roles)}' assigned to user");
        }
        return BadRequest(result.Errors);
    }

    [HttpPost("{userId}/claims")]
    public async Task<IActionResult> AddClaimToUser(string userId, [FromBody] WriteUserClaimDto claimDto)
    {
        if (claimDto == null)
        {
            return BadRequest("Claim data cannot be null.");
        }

        var result = await _accountService.AddClaimToUserAsync(userId, claimDto);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok("Claim successfully added to user.");
    }
}
