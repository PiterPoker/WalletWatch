using AuthWalletWatch.Application.DTOs.Base;

namespace AuthWalletWatch.Application.DTOs;

public record WriteProfileDto : ProfileDto, IWriteRecord
{
}

public record ReadProfileDto : ProfileDto, IReadRecord
{
    public Guid Id { get; init; }
}

public record ProfileDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public string? PhotoUrl { get; init; }
    public string? PhoneNumber { get; init; }
}
