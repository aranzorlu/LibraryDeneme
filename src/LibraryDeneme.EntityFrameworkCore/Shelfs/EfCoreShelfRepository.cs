using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using LibraryDeneme.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace LibraryDeneme.Shelfs;

public class EfCoreShelfRepository
    : EfCoreRepository<LibraryDenemeDbContext, Shelf, Guid>,
        IShelfRepository
{
    public EfCoreShelfRepository(
        IDbContextProvider<LibraryDenemeDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<Shelf> FindByNameAsync(string shelfname)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(shelf => shelf.ShelfName == shelfname);
    }

    public async Task<List<Shelf>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                shelf => shelf.ShelfName.Contains(filter)
                )
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
}
