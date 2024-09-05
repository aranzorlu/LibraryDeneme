using LibraryDeneme.Authors;
using LibraryDeneme.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using LibraryDeneme.Permissions;
using System.Linq.Dynamic.Core;
using LibraryDeneme.Shelfs;

namespace LibraryDeneme.Inventorys
{
    public class InventoryAppService :
     CrudAppService<
         Inventory, //The Book entity
         InventoryDto, //Used to show books
         Guid, //Primary key of the book entity
         PagedAndSortedResultRequestDto, //Used for paging/sorting
         CreateUpdateInventoryDto>, //Used to create/update a book
     IInventoryAppService //implement the IBookAppService
    {
		private readonly IAuthorRepository _authorRepository;
		private readonly IBookRepository _bookRepository;
		private readonly IShelfRepository _shelfRepository;

		public InventoryAppService(
			IRepository<Inventory, Guid> repository,
			IAuthorRepository authorRepository,
			IBookRepository bookRepository,
			IShelfRepository shelfRepository)
			: base(repository)
		{
			_shelfRepository = shelfRepository;
			_authorRepository = authorRepository;
			_bookRepository = bookRepository;
			GetPolicyName = LibraryDenemePermissions.Inventorys.Default;
			GetListPolicyName = LibraryDenemePermissions.Inventorys.Default;
			CreatePolicyName = LibraryDenemePermissions.Inventorys.Create;
			UpdatePolicyName = LibraryDenemePermissions.Inventorys.Edit;
			DeletePolicyName = LibraryDenemePermissions.Inventorys.Delete;
		}

		public override async Task<InventoryDto> GetAsync(Guid id)
		{
			//Get the IQueryable<Book> from the repository
			var queryable = await Repository.GetQueryableAsync();

			//Prepare a query to join books and authors
			var query = from inventory in queryable
						join book in await _bookRepository.GetQueryableAsync() on inventory.BookId equals book.Id
						join shelf in await _shelfRepository.GetQueryableAsync() on inventory.ShelfId equals shelf.Id
                        join author in await _authorRepository.GetQueryableAsync() on book.AuthorId equals author.Id
                        where inventory.Id == id
						select new { inventory,book, author,shelf };

			//Execute the query and get the book with author
			var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
			if (queryResult == null)
			{
				throw new EntityNotFoundException(typeof(Inventory), id);
			}

			var inventoryDto = ObjectMapper.Map<Inventory, InventoryDto>(queryResult.inventory);
			inventoryDto.ShelfName = queryResult.shelf.ShelfName;
			inventoryDto.BookName = queryResult.book.Name;
			inventoryDto.PublishDate = queryResult.book.PublishDate;
			inventoryDto.BookType = queryResult.book.Type;
            inventoryDto.AuthorName = queryResult.author.Name;
            return inventoryDto;
		}

		public override async Task<PagedResultDto<InventoryDto>> GetListAsync(PagedAndSortedResultRequestDto input)
		{
			//Get the IQueryable<Book> from the repository
			var queryable = await Repository.GetQueryableAsync();

			//Prepare a query to join books and authors
			var query = from inventory in queryable
						join book in await _bookRepository.GetQueryableAsync() on inventory.BookId equals book.Id
						join shelf in await _shelfRepository.GetQueryableAsync() on inventory.ShelfId equals shelf.Id
                        join author in await _authorRepository.GetQueryableAsync() on book.AuthorId equals author.Id
                        select new { inventory,book, author,shelf };

			//Paging
			query = query
			.OrderBy(NormalizeSorting(input.Sorting))
			.Skip(input.SkipCount)
			.Take(input.MaxResultCount);

			//Execute the query and get a list
			var queryResult = await AsyncExecuter.ToListAsync(query);

			//Convert the query result to a list of BookDto objects
			var inventoryDtos = queryResult.Select(x =>
			{
				var inventoryDto = ObjectMapper.Map<Inventory, InventoryDto>(x.inventory);
				inventoryDto.ShelfName = x.shelf.ShelfName;
				inventoryDto.BookName = x.book.Name;
                inventoryDto.PublishDate = x.book.PublishDate;
                inventoryDto.BookType = x.book.Type;
                inventoryDto.AuthorName = x.author.Name;
                return inventoryDto;
			}).ToList();

			//Get the total count with another query
			var totalCount = await Repository.GetCountAsync();

			return new PagedResultDto<InventoryDto>(
				totalCount,
				inventoryDtos
			);
		}

		
		public async Task<ListResultDto<BookLookupDto>> GetBookLookupAsync()
		{
			var books = await _bookRepository.GetListAsync();

			return new ListResultDto<BookLookupDto>(
				ObjectMapper.Map<List<Book>, List<BookLookupDto>>(books)
			);
		}
        public async Task<ListResultDto<ShelfLookupDto>> GetShelfLookupAsync()
        {
            var shelfs = await _shelfRepository.GetListAsync();

            return new ListResultDto<ShelfLookupDto>(
                ObjectMapper.Map<List<Shelf>, List<ShelfLookupDto>>(shelfs)
            );
        }

        private static string NormalizeSorting(string sorting)
		{
			if (sorting.IsNullOrEmpty())
			{
				return $"inventory.{nameof(Inventory.Id)}";
			}

			if (sorting.Contains("authorName", StringComparison.OrdinalIgnoreCase))
			{
				return sorting.Replace(
					"bookName",
					"book.Name",
					StringComparison.OrdinalIgnoreCase
				);
			}

			return $"inventory.{sorting}";
		}
	}
}
