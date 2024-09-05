using AutoMapper;
using LibraryDeneme.Authors;
using LibraryDeneme.Books;
using LibraryDeneme.Inventorys;
using LibraryDeneme.Shelfs;

namespace LibraryDeneme;

public class LibraryDenemeApplicationAutoMapperProfile : Profile
{
    public LibraryDenemeApplicationAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<Author, AuthorDto>();
        CreateMap<Author, AuthorLookupDto>();
        CreateMap<Shelf, ShelfDto>();
        CreateMap<Shelf, ShelfLookupDto>();
        CreateMap<Inventory, InventoryDto>();
        CreateMap<CreateUpdateInventoryDto, Inventory>();
		CreateMap<Book, BookLookupDto>();


		/* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
	}
}
