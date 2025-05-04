using AutoMapper;
using Expenses.Application.DTOs.Wallet;
using Expenses.Domain.Entities;

namespace Expenses.Application.Mappings;

/// <summary>
/// AutoMapper profile for the Wallet entity.
/// </summary>
public class WalletProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WalletProfile"/> class.
    /// </summary>
    public WalletProfile()
    {
        /// <summary>
        /// Maps <see cref="CreateWalletDto"/> to <see cref="Wallet"/>.
        /// </summary>
        CreateMap<CreateWalletDto, Wallet>();

        /// <summary>
        /// Maps <see cref="UpdateWalletDto"/> to <see cref="Wallet"/>.
        /// </summary>
        CreateMap<UpdateWalletDto, Wallet>();

        /// <summary>
        /// Maps <see cref="Wallet"/> to <see cref="WalletDto"/>.
        /// </summary>
        CreateMap<Wallet, WalletDto>().ReverseMap();
    }
}