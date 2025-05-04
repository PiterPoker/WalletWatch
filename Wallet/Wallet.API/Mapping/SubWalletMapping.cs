using AutoMapper;
using Wallet.API.Models.WalletOfFamily;
using Wallet.Domain.Models.Enums;
using WalletOfFamily = Wallet.Domain.Models.WalletOfFamily;

// Профиль для SubWallet и SubWalletWriteModel
public class SubWalletMapping : Profile
{
    public SubWalletMapping()
    {
        /*CreateMap<WalletOfFamily.SubWallet, SubWalletWriteModel>()
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
            //.ForMember(dest => dest.Family, opt => opt.MapFrom(src => new FamilyWriteModel { Name = src.Family.Name }))
            //.ForMember(dest => dest.ParentWallet, opt => opt.MapFrom(src => new WalletWriteModel { Description = src.ParentWallet.Description, Family = new FamilyWriteModel { Name = src.ParentWallet.Family.Name }, Balance = src.ParentWallet.Balance, CurrencyId = (int)src.ParentWallet.Currency }))
            .ForMember(dest => dest.FamilyMembers, opt => opt.MapFrom(src => src.FamilyMembers.Select(m => new OwnerWriteModel { Name = m.Name })));*/

        /*CreateMap<SubWalletWriteModel, WalletOfFamily.SubWallet>()
            .ForCtorParam("currency", opt => opt.MapFrom(src => src.Currency))
            //.ForCtorParam("family", opt => opt.MapFrom(src => new WalletOfFamily.Family(0, src.Family.Name)))
            .ForCtorParam("id", opt => opt.MapFrom(src => 0L))
            .ForCtorParam("balance", opt => opt.MapFrom(src => src.Balance));*/
        //.ForCtorParam("parentWallet", opt => opt.MapFrom(src => new WalletOfFamily.Wallet(0L, src.ParentWallet.Balance, (Currency)src.ParentWallet.CurrencyId, src.ParentWallet.Description)));

        CreateMap<SubWalletWriteModel, WalletOfFamily.SubWallet>();
        CreateMap<FamilyMemberWriteModel, WalletOfFamily.FamilyMember>();

        /*CreateMap<WalletOfFamily.SubWallet, SubWalletReadModel>()
            .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.Currency))
            //.ForMember(dest => dest.Family, opt => opt.MapFrom(src => new FamilyReadModel {  Id = src.Family.Id, Name = src.Family.Name }))
            //.ForMember(dest => dest.ParentWallet, opt => opt.MapFrom(src => new WalletReadModel { Id = src.ParentWallet.Id, Description = src.ParentWallet.Description, Family = new FamilyReadModel {Id = src.ParentWallet.Family.Id, Name = src.ParentWallet.Family.Name }, Balance = src.ParentWallet.Balance, CurrencyId = (int)src.ParentWallet.Currency }))
            .ForMember(dest => dest.FamilyMembers, opt => opt.MapFrom(src => src.FamilyMembers.Select(m => new OwnerReadModel { Id = m.Id, Name = m.Name })));*/

        CreateMap<WalletOfFamily.SubWallet, SubWalletReadModel>()
            .MaxDepth(3);
        CreateMap<WalletOfFamily.FamilyMember, FamilyMemberReadModel>()
            .MaxDepth(3);

        /*CreateMap<SubWalletReadModel, WalletOfFamily.SubWallet>()
            .ForCtorParam("currency", opt => opt.MapFrom(src => (Currency)src.CurrencyId))
            //.ForCtorParam("family", opt => opt.MapFrom(src => new WalletOfFamily.Family(src.Family.Id, src.Family.Name)))
            .ForCtorParam("id", opt => opt.MapFrom(src => src.Id))
            .ForCtorParam("balance", opt => opt.MapFrom(src => src.Balance))
            .ForCtorParam("parentWallet", opt => opt.MapFrom(src => new WalletOfFamily.Wallet(src.ParentWallet.Id, src.ParentWallet.Balance, (Currency)src.ParentWallet.CurrencyId, src.ParentWallet.Description)))
            .ForMember(dest => dest.FamilyMembers, opt => opt.Ignore());*/
    }
}
