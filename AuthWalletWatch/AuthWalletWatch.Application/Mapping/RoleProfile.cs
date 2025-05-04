using AuthWalletWatch.Application.DTOs;
using AuthWalletWatch.Infrastructure.Models;
using AutoMapper;

namespace AuthWalletWatch.Application.Mapping;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<ApplicationRole, ReadRoleDto>()
            .ForMember(dest => dest.Claims, opt => opt.MapFrom(src => src.RoleClaims.Select(uc => new ReadClaimDto { Id = uc.Id, ClaimType = uc.ClaimType, ClaimValue = uc.ClaimValue })));
        CreateMap<WriteRoleDto, ApplicationRole>()
            .ForMember(dest => dest.RoleClaims, opt => opt.Ignore());
    }
}
