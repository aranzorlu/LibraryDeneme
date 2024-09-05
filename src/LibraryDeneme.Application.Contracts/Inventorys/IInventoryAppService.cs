using LibraryDeneme.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LibraryDeneme.Inventorys
{
    public interface IInventoryAppService :
    ICrudAppService< //Defines CRUD methods
        InventoryDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateInventoryDto> //Used to create/update a book
    {
		//Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync();
		Task<ListResultDto<BookLookupDto>> GetBookLookupAsync();
        Task<ListResultDto<ShelfLookupDto>> GetShelfLookupAsync();
    }
}
