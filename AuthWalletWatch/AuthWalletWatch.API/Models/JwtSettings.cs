namespace AuthWalletWatch.API.Models;

public class JwtSettings
{
    public required string SecretKey { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public long AccessTokenExpirationSeconds { get; init; }
    public int RefreshTokenExpirationDays { get; init; }
}
