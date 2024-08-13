using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryDeneme.Authors;
using LibraryDeneme.Permissions;

using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace LibraryDeneme.Shelfs;

[Authorize(LibraryDenemePermissions.Shelfs.Default)]
public class ShelfAppService : LibraryDenemeAppService, IShelfAppService
{
    private readonly IShelfRepository _shelfRepository;
    private readonly ShelfManager _shelfManager;

    public ShelfAppService(
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
            input.ShelfType
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

    //...SERVICE METHODS WILL COME HERE...
}