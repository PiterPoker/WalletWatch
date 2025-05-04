namespace AuthWalletWatch.Application.DTOs;

public record ClaimDto
{
    public string? ClaimType { get; init; }
    public string? ClaimValue { get; init; }
}

public record ReadClaimDto : ClaimDto
{
    public int Id { get; init; }
}

public record WriteClaimDto : ClaimDto
{
}

public record ReadRoleClaimDto : ReadClaimDto
{
    public Guid RoleId { get; init; }
}

public record WriteRoleClaimDto : WriteClaimDto
{
}

public record ReadUserClaimDto : ReadClaimDto
{
    public Guid UserId { get; init; }
}

public record WriteUserClaimDto : WriteClaimDto
{
}