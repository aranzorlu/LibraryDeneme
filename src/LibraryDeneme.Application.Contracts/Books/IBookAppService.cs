using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace LibraryDeneme.Books;

public interface IBookAppService :
    ICrudAppService< //Defines CRUD methods
        BookDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateBookDto> //Used to create/update a book
        

{
    [AllowAnonymous]
    Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync();

    [AllowAnonymous]
    Task<ListResultDto<ShelfLookupDto>> GetShelfLookupAsync();
    [AllowAnonymous]
    Task<PagedResultDto<BookDto>> GetBooksByLibraryAsync(BolumType libraryName, PagedAndSortedResultRequestDto input);

}