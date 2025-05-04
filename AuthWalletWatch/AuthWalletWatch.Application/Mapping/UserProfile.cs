using AuthWalletWatch.Application.DTOs;
using AuthWalletWatch.Infrastructure.Models;
using AutoMapper;

namespace AuthWalletWatch.Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, ReadUserDto>()
            .ForMember(dest => dest.Profile, opt => opt.MapFrom(src => src.Profile))
            .ForMember(dest => dest.Claims, opt => opt.MapFrom(src => src.Claims.Select(uc => new ReadClaimDto { Id = uc.Id, ClaimType = uc.ClaimType, ClaimValue = uc.ClaimValue })))
            /*.ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(ur => new ReadRoleDto { Id = ur.Id })))*/; // Предполагается, что ReadRoleDto имеет Id
        // Для обратного маппинга (WriteUserDto -> ApplicationUser) нужно учитывать,
        // что Claims и Roles являются коллекциями и могут требовать дополнительной логики.
        CreateMap<WriteUserDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.ProfileId, opt => opt.MapFrom(src => src.ProfileId))
            // Для Claims и Roles при создании обычно используется UserManager.AddToClaim/AddToRole
            // поэтому здесь мы можем не маппить напрямую коллекции.
            .ForMember(dest => dest.Claims, opt => opt.Ignore())
            .ForMember(dest => dest.Roles, opt => opt.Ignore());

        CreateMap<ReadUserDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

        CreateMap<ApplicationUser, WriteUserDto>();
    }
}