using AuthWalletWatch.Application.DTOs.Base;

namespace AuthWalletWatch.Application.DTOs;

public record RoleDto
{
    public string? Name { get; init; }
    public string? Description { get; init; }
}

public record ReadRoleDto : RoleDto, IReadRecord
{
    public required Guid Id { get; init; }
    public List<ReadClaimDto>? Claims { get; set; }
}

public record WriteRoleDto : RoleDto, IWriteRecord
{
}