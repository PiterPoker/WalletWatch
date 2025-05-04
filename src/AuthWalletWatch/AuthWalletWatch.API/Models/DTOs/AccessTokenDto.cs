namespace AuthWalletWatch.API.Models.DTOs;

public record AccessTokenDto
{
    public string? TokenType { get; init; }
    public required string AccessToken { get; init; }
    public long ExpiresIn { get; init; }
    public string? RefreshToken { get; init; }
}