using AutoMapper;
using Expenses.Application.DTOs.Author;
using Expenses.Domain.Entities;

namespace Expenses.Application.Mappings;

/// <summary>
/// AutoMapper profile for the Author entity.
/// </summary>
public class AuthorProfile : Profile
{
    /// <summary>
    /// Constructor for AuthorProfile.
    /// </summary>
    public AuthorProfile()
    {
        // Mapping from CreateAuthorDto to Author.
        CreateMap<CreateAuthorDto, Author>();

        // Mapping from UpdateAuthorDto to Author.
        CreateMap<UpdateAuthorDto, Author>();

        // Bidirectional mapping between Author and AuthorDto.
        CreateMap<Author, AuthorDto>().ReverseMap();
    }
}