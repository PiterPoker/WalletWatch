using System.ComponentModel.DataAnnotations;

namespace AuthWalletWatch.API.Models.DTOs;

public record LoginDto
{
    public required string Username { get; init; }
    [DataType(DataType.Password)]
    public required string Password { get; init; }
}
