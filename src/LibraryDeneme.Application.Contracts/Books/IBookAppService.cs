using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Microsoft.AspNetCore.Authorization;
using LibraryDeneme.Authors;

namespace LibraryDeneme.Books;

public interface IBookAppService : IApplicationService
{
    Task<BookDto> GetAsync(Guid id);

    Task<PagedResultDto<BookDto>> GetListAsync(GetBookListDto input);

    Task<BookDto> CreateAsync(CreateBookDto input);

    Task UpdateAsync(Guid id, UpdateBookDto input);

    Task DeleteAsync(Guid id);

    Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync();
    
}