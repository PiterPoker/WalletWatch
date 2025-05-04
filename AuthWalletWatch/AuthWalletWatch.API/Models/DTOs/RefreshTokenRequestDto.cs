namespace AuthWalletWatch.API.Models.DTOs
{
    public record RefreshTokenRequestDto
    {
        public required string RefreshToken { get; set; }
    }
}
