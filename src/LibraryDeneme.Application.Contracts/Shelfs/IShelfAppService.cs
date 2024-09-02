
using LibraryDeneme.Books;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LibraryDeneme.Shelfs;

public interface IShelfAppService : IApplicationService
{
    Task<ShelfDto> GetAsync(Guid id);

    Task<PagedResultDto<ShelfDto>> GetListAsync(GetShelfListDto input);

    Task<ShelfDto> CreateAsync(CreateShelfDto input);

    Task UpdateAsync(Guid id, UpdateShelfDto input);

    Task DeleteAsync(Guid id);
    Task<PagedResultDto<ShelfDto>> GetShelfsByLibraryAsync(BolumType libraryName, PagedAndSortedResultRequestDto input);
}
