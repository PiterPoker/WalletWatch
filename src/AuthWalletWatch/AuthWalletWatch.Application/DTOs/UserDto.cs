using AuthWalletWatch.Application.DTOs.Base;

namespace AuthWalletWatch.Application.DTOs;

public record UserDto
{
    public string? UserName { get; init; }
    public string? Email { get; init; }
}

public record WriteUserDto : UserDto, IWriteRecord
{
    public WriteProfileDto? Profile { get; init; }
    public Guid ProfileId { get; init; }
    public List<WriteClaimDto>? Claims { get; init; }
    public List<WriteRoleDto>? Roles { get; init; }
}

public record ReadUserDto : UserDto, IReadRecord
{
    public Guid Id { get; init; }
    public ReadProfileDto? Profile { get; init; }
    public List<ReadClaimDto>? Claims { get; init; }
    public List<ReadRoleDto>? Roles { get; init; }
}