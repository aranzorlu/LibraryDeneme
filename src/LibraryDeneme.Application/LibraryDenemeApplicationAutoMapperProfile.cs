using AutoMapper;
using LibraryDeneme.Authors;
using LibraryDeneme.Books;
using LibraryDeneme.Shelfs;

namespace LibraryDeneme;

public class LibraryDenemeApplicationAutoMapperProfile : Profile
{
    public LibraryDenemeApplicationAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        CreateMap<Author, AuthorDto>();
        CreateMap<Author, AuthorLookupDto>();
        CreateMap<Shelf, ShelfDto>();
        CreateMap<Shelf, ShelfLookupDto>();
      
        

        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}
