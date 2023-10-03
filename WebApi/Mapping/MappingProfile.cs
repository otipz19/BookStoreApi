using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace WebApi.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Author, AuthorDto>().ReverseMap();

        CreateMap<Genre, GenreDto>().ReverseMap();

        CreateMap<CreateBookDto, Book>();

        CreateMap<Book, GetBookDto>()
            .ForMember(dest => dest.AuthorName,
                opt => opt.MapFrom(src => src.Author.Name))
            .ForMember(dest => dest.GenreName,
                opt => opt.MapFrom(src => src.Genre.Name));
    }
}