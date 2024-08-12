using AutoMapper;
using LibraryDeneme.Authors;
using LibraryDeneme.Books;
using LibraryDeneme.Shelfs;

namespace LibraryDeneme.Blazor;

public class LibraryDenemeBlazorAutoMapperProfile : Profile
{
    public LibraryDenemeBlazorAutoMapperProfile()
    {
        CreateMap<BookDto, CreateUpdateBookDto>();
        CreateMap<AuthorDto, UpdateAuthorDto>();
        CreateMap<ShelfDto, CreateUpdateShelfDto>();

        //Define your AutoMapper configuration here for the Blazor project.
    }
}
