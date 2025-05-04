using AutoMapper;
using Wallet.API.Models.WalletOfFamily;
using Wallet.Domain.Models.Enums;
using Wallet.Domain.Models.WalletOfFamily;
using WalletOfFamily = Wallet.Domain.Models.WalletOfFamily;

namespace Wallet.API.Mapping;

public class WalletMapping : Profile
{
    public WalletMapping()
    {
        // Сопоставление из доменной модели в модель представления
        /*CreateMap<WalletOfFamily.Wallet, WalletWriteModel>()
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => (int)src.Currency));*/ // Преобразование Currency в int
                                                                                               //.ForMember(dest => dest.Family, opt => opt.MapFrom(src => new FamilyWriteModel { Id = src.Family.Id, Name = src.Family.Name }));

        // Сопоставление из модели представления в доменную модель
        CreateMap<WalletWriteModel, WalletOfFamily.Wallet>();
        /*.ForCtorParam("currency", opt => opt.MapFrom(src => src.Currency)) // Преобразование int обратно в Currency
        //.ForCtorParam("family", opt => opt.MapFrom(src => new Family(src.Family.Id, src.Family.Name))) // Пример создания объекта Family. В реальном коде логика может быть другой.
        .ForCtorParam("id", opt => opt.MapFrom(src => 0L))
        .ForCtorParam("balance", opt => opt.MapFrom(src => src.Balance));*/

        // Сопоставление из доменной модели в модель представления
        CreateMap<WalletOfFamily.Wallet, WalletReadModel>().MaxDepth(3);
            //.ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => (int)src.Currency)); // Преобразование Currency в int
            //.ForMember(dest => dest.Family, opt => opt.MapFrom(src => new FamilyReadModel { Id = src.Family.Id , Name = src.Family.Name }))
            //.ForMember(dest => dest.SubWallets, opt => opt.MapFrom(src => src.SubWallets.Select(sw => new WalletReadModel { Id = sw.Id, Balance = sw.Balance, CurrencyId = (int)sw.Currency, Description = sw.Description, Family = new FamilyReadModel { Id = sw.Family.Id, Name = sw.Family.Name } }))); // Преобразование SubWallets

        // Сопоставление из модели представления в доменную модель
        /*CreateMap<WalletReadModel, WalletOfFamily.Wallet>()
            .ForCtorParam("currency", opt => opt.MapFrom(src => (Currency)src.CurrencyId)) // Преобразование int обратно в Currency
            //.ForCtorParam("family", opt => opt.MapFrom(src => new Family(src.Family.Id, src.Family.Name))) // Пример создания объекта Family. В реальном коде логика может быть другой.
            .ForCtorParam("id", opt => opt.MapFrom(src => src.Id)) 
            .ForCtorParam("balance", opt => opt.MapFrom(src => src.Balance));*/
    }
}
