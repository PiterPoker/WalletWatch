using AuthWalletWatch.Application.DTOs;
using AuthWalletWatch.Infrastructure.Models;
using AutoMapper;

namespace AuthWalletWatch.Application.Mapping;

public class ProfileProfile : Profile
{
    public ProfileProfile()
    {
        CreateMap<ApplicationProfile, ReadProfileDto>();
        CreateMap<WriteProfileDto, ApplicationProfile>();
    }
}