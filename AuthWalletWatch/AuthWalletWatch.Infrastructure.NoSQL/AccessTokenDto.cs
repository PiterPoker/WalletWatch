namespace AuthWalletWatch.Infrastructure.NoSQL;

internal record AccessTokenDto
{
    public required string TokenType { get; init; }
    public required string AccessToken { get; init; }
    public DateTime ExpiresIn { get; init; }
}
