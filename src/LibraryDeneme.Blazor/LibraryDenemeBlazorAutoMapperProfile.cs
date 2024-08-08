using AutoMapper;
using LibraryDeneme.Authors;
using LibraryDeneme.Books;

namespace LibraryDeneme.Blazor;

public class LibraryDenemeBlazorAutoMapperProfile : Profile
{
    public LibraryDenemeBlazorAutoMapperProfile()
    {
        CreateMap<BookDto, CreateUpdateBookDto>();
        CreateMap<AuthorDto, UpdateAuthorDto>();

        //Define your AutoMapper configuration here for the Blazor project.
    }
}
