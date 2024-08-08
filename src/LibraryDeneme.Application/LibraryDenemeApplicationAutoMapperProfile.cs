using AutoMapper;
using LibraryDeneme.Authors;
using LibraryDeneme.Books;

namespace LibraryDeneme;

public class LibraryDenemeApplicationAutoMapperProfile : Profile
{
    public LibraryDenemeApplicationAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        CreateMap<Author, AuthorDto>();
        CreateMap<Author, AuthorLookupDto>();
        

        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}
