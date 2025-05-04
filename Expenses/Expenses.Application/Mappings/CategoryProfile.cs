using AutoMapper;
using Expenses.Application.DTOs.Category;
using Expenses.Domain.Entities;
using System.Drawing;

namespace Expenses.Application.Mappings;

/// <summary>
/// AutoMapper profile for mapping Category-related DTOs to Category entities and vice versa.
/// </summary>
public class CategoryProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryProfile"/> class, defining the mapping configurations.
    /// </summary>
    public CategoryProfile()
    {
        /// <summary>
        /// Maps <see cref="CreateCategoryDto"/> to <see cref="Category"/>, ignoring the Author property and mapping the Color property.
        /// </summary>
        CreateMap<CreateCategoryDto, Category>()
            .ForMember(dest => dest.Author, opt => opt.Ignore());
        //.ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color != null ? Color.FromName(src.Color) : (Color?)null));

        /// <summary>
        /// Maps <see cref="UpdateCategoryDto"/> to <see cref="Category"/>, ignoring the Author property and mapping the Color property.
        /// </summary>
        CreateMap<UpdateCategoryDto, Category>();
            //.ForMember(dest => dest.Author, opt => opt.Ignore());
            //.ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color != null ? Color.FromName(src.Color) : (Color?)null));

        /// <summary>
        /// Maps <see cref="Category"/> to <see cref="CategoryDto"/>, mapping the AuthorId and Color properties.
        /// </summary>
        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Author.Id));
            //.ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color.HasValue ? src.Color.Value.Name : null));
    }
}