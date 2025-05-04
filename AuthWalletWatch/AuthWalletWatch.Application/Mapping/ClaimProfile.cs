using AuthWalletWatch.Application.DTOs;
using AuthWalletWatch.Infrastructure.Models;
using AutoMapper;

namespace AuthWalletWatch.Application.Mapping;

public class ClaimProfile : Profile
{
    public ClaimProfile()
    {
        CreateMap<ApplicationRoleClaim, ReadRoleClaimDto>();
        CreateMap<WriteRoleClaimDto, ApplicationRoleClaim>();
        CreateMap<ApplicationUserClaim, ReadUserClaimDto>();
        CreateMap<WriteUserClaimDto, ApplicationUserClaim>();
    }
}