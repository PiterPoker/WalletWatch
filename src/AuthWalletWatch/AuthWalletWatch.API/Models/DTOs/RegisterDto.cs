using System.ComponentModel.DataAnnotations;

namespace AuthWalletWatch.API.Models.DTOs;

public record RegisterDto
{
    public required string Username { get; init; }
    [EmailAddress]
    public required string Email { get; init; }
    [DataType(DataType.Password)]
    public required string Password { get; init; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public required string ConfirmPassword { get; init; }
}
