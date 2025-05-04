using AutoMapper;
using Expenses.Application.DTOs.Expense;
using Expenses.Domain.Entities;

namespace Expenses.Application.Mappings;

/// <summary>
/// AutoMapper profile for mapping Expense entities to Expense DTOs.
/// </summary>
public class ExpenseProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExpenseProfile"/> class, defining the mapping configurations.
    /// </summary>
    public ExpenseProfile()
    {
        CreateMap<Expense, ExpenseDto>()
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount.Value))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Amount.Currency))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
    }
}