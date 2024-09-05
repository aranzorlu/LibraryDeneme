using AutoMapper;
using LibraryDeneme.Authors;
using LibraryDeneme.Books;
using LibraryDeneme.Inventorys;
using LibraryDeneme.Shelfs;

namespace LibraryDeneme.Blazor;

public class LibraryDenemeBlazorAutoMapperProfile : Profile
{
    public LibraryDenemeBlazorAutoMapperProfile()
    {
        CreateMap<BookDto, UpdateBookDto>();
        CreateMap<AuthorDto, UpdateAuthorDto>();
        CreateMap<ShelfDto, UpdateShelfDto>();
        CreateMap<InventoryDto,CreateUpdateInventoryDto>();

        //Define your AutoMapper configuration here for the Blazor project.
    }
}
