using AutoMapper;
using Wallet.API.Models.AccountOfPerson;
using Wallet.Domain.Models.AccountOfPerson;

namespace Wallet.API.Mapping;

public class AccountMapping : Profile
{
    public AccountMapping()
    {
        // Маппинг для создания нового счета
        CreateMap<AccountWriteModel, Account>();
            /*.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
            .ForMember(dest => dest.ProfileId, opt => opt.MapFrom(src => src.ProfileId));*/

        // Маппинг для возврата информации о счете
        CreateMap<Account, AccountReadModel>();
            /*.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
            .ForMember(dest => dest.ProfileId, opt => opt.MapFrom(src => src.ProfileId));*/
    }
}
