using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryDeneme.Authors;
using LibraryDeneme.Books;
using LibraryDeneme.Permissions;

using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;

namespace LibraryDeneme.Shelfs;

[Authorize(LibraryDenemePermissions.Shelfs.Default)]
public class ShelfAppService : LibraryDenemeAppService, IShelfAppService
{
    private readonly IShelfRepository _shelfRepository;
    private readonly ShelfManager _shelfManager;

    public ShelfAppService(
        IRepository<Book, Guid> repository,
        IShelfRepository shelfRepository,
        ShelfManager shelfManager)
        
    {
        _shelfRepository = shelfRepository;
        _shelfManager = shelfManager;
    }
    public async Task<ShelfDto> GetAsync(Guid id)
    {
        var shelf = await _shelfRepository.GetAsync(id);
        return ObjectMapper.Map<Shelf, ShelfDto>(shelf);
    }
    [AllowAnonymous]
    public async Task<PagedResultDto<ShelfDto>> GetListAsync(GetShelfListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Shelf.ShelfName);
        }

        var shelfs = await _shelfRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
        );

        var totalCount = input.Filter == null
            ? await _shelfRepository.CountAsync()
            : await _shelfRepository.CountAsync(
                shelf => shelf.ShelfName.Contains(input.Filter));

        return new PagedResultDto<ShelfDto>(
            totalCount,
            ObjectMapper.Map<List<Shelf>, List<ShelfDto>>(shelfs)
        );
    }
    [Authorize(LibraryDenemePermissions.Shelfs.Create)]
    public async Task<ShelfDto> CreateAsync(CreateShelfDto input)
    {
        var shelf = await _shelfManager.CreateAsync(
            input.ShelfName,
            input.ShelfType,
            input.ShelfBolum
            
        );

        await _shelfRepository.InsertAsync(shelf);

        return ObjectMapper.Map<Shelf, ShelfDto>(shelf);
    }

    [Authorize(LibraryDenemePermissions.Shelfs.Edit)]
    public async Task UpdateAsync(Guid id, UpdateShelfDto input)
    {
        var shelf = await _shelfRepository.GetAsync(id);

        if (shelf.ShelfName != input.ShelfName)
        {
            await _shelfManager.ChangeNameAsync(shelf, input.ShelfName);
        }

        shelf.ShelfType = input.ShelfType;
        

        await _shelfRepository.UpdateAsync(shelf);
    }
    [Authorize(LibraryDenemePermissions.Shelfs.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _shelfRepository.DeleteAsync(id);
    }
    [AllowAnonymous]
    public async Task<PagedResultDto<ShelfDto>> GetShelfsByLibraryAsync(BolumType libraryName, PagedAndSortedResultRequestDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Shelf.ShelfName); // Varsayılan sıralama kriteri
        }
        // Get the IQueryable<Book> from the repository
        var queryable = await _shelfRepository.GetQueryableAsync();
        var query = queryable.Where(shelf => shelf.ShelfBolum == libraryName);

        var shelfs = await AsyncExecuter.ToListAsync(
            query.OrderBy(input.Sorting)
                 .Skip(input.SkipCount)
                 .Take(input.MaxResultCount)
        );

        var totalCount = await AsyncExecuter.CountAsync(query);

        return new PagedResultDto<ShelfDto>(
            totalCount,
            ObjectMapper.Map<List<Shelf>, List<ShelfDto>>(shelfs)
        );
    }

    //...SERVICE METHODS WILL COME HERE...
}