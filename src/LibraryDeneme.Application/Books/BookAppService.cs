using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using LibraryDeneme.Permissions;
using LibraryDeneme.Authors;
using LibraryDeneme.Books;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using AutoMapper.Internal.Mappers;
using Volo.Abp.ObjectMapping;
using LibraryDeneme.Shelfs;
namespace LibraryDeneme.Books;


[Authorize(LibraryDenemePermissions.Books.Default)]
public class BookAppService : LibraryDenemeAppService, IBookAppService
{
    private readonly IBookRepository _bookRepository;
    private readonly BookManager _bookManager;
    private readonly IAuthorRepository _authorRepository;

    public BookAppService(
        IBookRepository bookRepository,
        BookManager bookManager,
        IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _bookManager = bookManager;
        _authorRepository = authorRepository;
    }
    public async Task<BookDto> GetAsync(Guid id)
    {
        var bookQueryable = await _bookRepository.GetQueryableAsync();
        
        // Kitaplar ve yazarlar arasında bir sorgu hazırlıyoruz
        var query = from book in bookQueryable
                    join author in await _authorRepository.GetQueryableAsync() on book.AuthorId equals author.Id
                    where book.Id == id
                    select new { book, author };

        // Sorguyu çalıştırıyoruz ve sonuçları alıyoruz
        var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
        if (queryResult == null)
        {
            throw new EntityNotFoundException(typeof(Book), id);
        }

        var bookDto = ObjectMapper.Map<Book, BookDto>(queryResult.book);
        bookDto.AuthorName = queryResult.author.Name;
        return bookDto;
    }
    
    public async Task<PagedResultDto<BookDto>> GetListAsync(GetBookListDto input)
    {
        var queryable = await _bookRepository.GetQueryableAsync();

        //Prepare a query to join books and authors
        var query = from book in queryable
                    join author in await _authorRepository.GetQueryableAsync() on book.AuthorId equals author.Id
                    select new { book, author };

        //Paging
        query = query
            .OrderBy(NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        //Execute the query and get a list
        var queryResult = await AsyncExecuter.ToListAsync(query);

        //Convert the query result to a list of BookDto objects
        var bookDtos = queryResult.Select(x =>
        {
            var bookDto = ObjectMapper.Map<Book, BookDto>(x.book);
            bookDto.AuthorName = x.author.Name;
            return bookDto;
        }).ToList();

        //Get the total count with another query
        var totalCount = await _bookRepository.GetCountAsync();

        return new PagedResultDto<BookDto>(
            totalCount,
            bookDtos
        );
    }
    [Authorize(LibraryDenemePermissions.Books.Create)]
    public async Task<BookDto> CreateAsync(CreateBookDto input)
    {
        var book = await _bookManager.CreateAsync(
            input.AuthorId,
            input.Name,
            input.Type,
            input.PublishDate
        );

        await _bookRepository.InsertAsync(book);

        return ObjectMapper.Map<Book, BookDto>(book);
    }
    [Authorize(LibraryDenemePermissions.Books.Edit)]
    public async Task UpdateAsync(Guid id, UpdateBookDto input)
    {
        var books = await _bookRepository.GetAsync(id);

        if (books.Name != input.Name)
        {
            await _bookManager.ChangeNameAsync(books, input.Name);
        }
        books.AuthorId = input.AuthorId;
        books.Type = input.Type;
        books.PublishDate = input.PublishDate;

        await _bookRepository.UpdateAsync(books);
    }

    [Authorize(LibraryDenemePermissions.Books.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _bookRepository.DeleteAsync(id);
    }
    public async Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()
    {
        var authors = await _authorRepository.GetListAsync();

        return new ListResultDto<AuthorLookupDto>(
            ObjectMapper.Map<List<Author>, List<AuthorLookupDto>>(authors)
        );
    }
    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"book.{nameof(Book.Name)}";
        }

        if (sorting.Contains("authorName", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "authorName",
                "author.Name",
                StringComparison.OrdinalIgnoreCase
            );
        }

        return $"book.{sorting}";
    }

}

    /*
    [AllowAnonymous]
    public class BookAppService :
        CrudAppService<
            Book, //The Book entity
            BookDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateBookDto>, //Used to create/update a book
        IBookAppService //implement the IBookAppService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IShelfRepository _shelfRepository;

        public BookAppService(
            IRepository<Book, Guid> repository,
            IShelfRepository shelfRepository,
            IAuthorRepository authorRepository)
            : base(repository)
        {
            _shelfRepository = shelfRepository;
            _authorRepository = authorRepository;
            GetPolicyName = LibraryDenemePermissions.Books.Default;
            GetListPolicyName = LibraryDenemePermissions.Books.Default;
            CreatePolicyName = LibraryDenemePermissions.Books.Create;
            UpdatePolicyName = LibraryDenemePermissions.Books.Edit;
            DeletePolicyName = LibraryDenemePermissions.Books.Delete;
        }

        public override async Task<BookDto> GetAsync(Guid id)
        {
            //Get the IQueryable<Book> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join books, authors, and shelves
            var query = from book in queryable
                        join author in await _authorRepository.GetQueryableAsync() on book.AuthorId equals author.Id
                        //join shelf in await _shelfRepository.GetQueryableAsync() on book.ShelfId equals shelf.Id
                        where book.Id == id
                        select new { book, author/* shelf*/ //};

    //Execute the query and get the book with author and shelf
    /*        var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Book), id);
            }

            var bookDto = ObjectMapper.Map<Book, BookDto>(queryResult.book);
            bookDto.AuthorName = queryResult.author.Name;
            //bookDto.ShelfName = queryResult.shelf.ShelfName; // Shelf adını BookDto'ya ekliyoruz
            return bookDto;
        }


        public override async Task<PagedResultDto<BookDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            //Get the IQueryable<Book> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join books, authors, and shelves
            var query = from book in queryable
                        join author in await _authorRepository.GetQueryableAsync() on book.AuthorId equals author.Id
                       // join shelf in await _shelfRepository.GetQueryableAsync() on book.ShelfId equals shelf.Id
                        select new { book, author /*shelf*/
//};

        //Paging
  /*      query = query
            .OrderBy(NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        //Execute the query and get a list
        var queryResult = await AsyncExecuter.ToListAsync(query);

        //Convert the query result to a list of BookDto objects
        var bookDtos = queryResult.Select(x =>
        {
            var bookDto = ObjectMapper.Map<Book, BookDto>(x.book);
            bookDto.AuthorName = x.author.Name;
            //bookDto.ShelfName = x.shelf.ShelfName; // Shelf adını BookDto'ya ekliyoruz
            return bookDto;
        }).ToList();

        //Get the total count with another query
        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<BookDto>(
            totalCount,
            bookDtos
        );
    }

    public async Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()
    {
        var authors = await _authorRepository.GetListAsync();

        return new ListResultDto<AuthorLookupDto>(
            ObjectMapper.Map<List<Author>, List<AuthorLookupDto>>(authors)
        );
    }

    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"book.{nameof(Book.Name)}";
        }

        if (sorting.Contains("authorName", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "authorName",
                "author.Name",
                StringComparison.OrdinalIgnoreCase
            );
        }
        if (sorting.Contains("shelfName", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "shelfName",
                "shelf.ShelfName",
                StringComparison.OrdinalIgnoreCase
            );
        }

        return $"book.{sorting}";
    }
    public async Task<ListResultDto<ShelfLookupDto>> GetShelfLookupAsync()
    {
        // Shelf'leri alıyoruz
        var shelfs = await _shelfRepository.GetListAsync();

        // Shelf'leri ShelfLookupDto'ya map ediyoruz ve döndürüyoruz
        return new ListResultDto<ShelfLookupDto>(
            ObjectMapper.Map<List<Shelf>, List<ShelfLookupDto>>(shelfs)
        );
    }
    /*public async Task<PagedResultDto<BookDto>> GetBooksByLibraryAsync(BolumType libraryName, PagedAndSortedResultRequestDto input)
    {
        // Get the IQueryable<Book> from the repository
        var queryable = await Repository.GetQueryableAsync();

        // Get the enum value as string
        

        // Filter the books by the specified library name (Bolum)
        var query = from book in queryable
                    join author in await _authorRepository.GetQueryableAsync() on book.AuthorId equals author.Id
                    //join shelf in await _shelfRepository.GetQueryableAsync() on book.ShelfId equals shelf.Id
                    //where book.Bolum == libraryName
                    select new { book, author, /*shelf*/ //};

        // Apply paging and sorting
      /*  query = query
            .OrderBy(NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        // Execute the query and get the list of books
        var queryResult = await AsyncExecuter.ToListAsync(query);

        // Convert the result to BookDto list
        var bookDtos = queryResult.Select(x =>
        {
            var bookDto = ObjectMapper.Map<Book, BookDto>(x.book);
            bookDto.AuthorName = x.author.Name;
           // bookDto.ShelfName = x.shelf.ShelfName;
            return bookDto;
        }).ToList();

        // Get the total count of books in the specified library
        //var totalCount = await Repository.CountAsync(book => book.Bolum == libraryName);

       // return new PagedResultDto<BookDto>(
         //   totalCount,
           // bookDtos
        //);
    }



}
*/


// Diğer metodlar da benzer şekilde güncellenmeli