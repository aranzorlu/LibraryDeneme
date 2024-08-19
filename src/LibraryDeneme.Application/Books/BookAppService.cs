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
                    join shelf in await _shelfRepository.GetQueryableAsync() on book.ShelfId equals shelf.Id
                    where book.Id == id
                    select new { book, author, shelf };

        //Execute the query and get the book with author and shelf
        var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
        if (queryResult == null)
        {
            throw new EntityNotFoundException(typeof(Book), id);
        }

        var bookDto = ObjectMapper.Map<Book, BookDto>(queryResult.book);
        bookDto.AuthorName = queryResult.author.Name;
        bookDto.ShelfName = queryResult.shelf.ShelfName; // Shelf adını BookDto'ya ekliyoruz
        return bookDto;
    }


    public override async Task<PagedResultDto<BookDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        //Get the IQueryable<Book> from the repository
        var queryable = await Repository.GetQueryableAsync();

        //Prepare a query to join books, authors, and shelves
        var query = from book in queryable
                    join author in await _authorRepository.GetQueryableAsync() on book.AuthorId equals author.Id
                    join shelf in await _shelfRepository.GetQueryableAsync() on book.ShelfId equals shelf.Id
                    select new { book, author, shelf };

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
            bookDto.ShelfName = x.shelf.ShelfName; // Shelf adını BookDto'ya ekliyoruz
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
    public async Task<PagedResultDto<BookDto>> GetBooksByLibraryAsync(string libraryName, PagedAndSortedResultRequestDto input)
    {
        // Get the IQueryable<Book> from the repository
        var queryable = await Repository.GetQueryableAsync();

        // Filter the books by the specified library name (Bolum)
        var query = from book in queryable
                    join author in await _authorRepository.GetQueryableAsync() on book.AuthorId equals author.Id
                    join shelf in await _shelfRepository.GetQueryableAsync() on book.ShelfId equals shelf.Id
                    where book.Bolum == libraryName
                    select new { book, author, shelf };

        // Apply paging and sorting
        query = query
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
            bookDto.ShelfName = x.shelf.ShelfName;
            return bookDto;
        }).ToList();

        // Get the total count of books in the specified library
        var totalCount = await Repository.CountAsync(book => book.Bolum == libraryName);

        return new PagedResultDto<BookDto>(
            totalCount,
            bookDtos
        );
    }


}



// Diğer metodlar da benzer şekilde güncellenmeli