using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using LibraryDeneme.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace LibraryDeneme.Books;

public class EfCoreBookRepository
    : EfCoreRepository<LibraryDenemeDbContext, Book, Guid>,
        IBookRepository
{
    public EfCoreBookRepository(
        IDbContextProvider<LibraryDenemeDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<Book> FindByNameAsync(string name)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(book => book.Name == name);
    }

    public async Task<List<Book>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                book => book.Name.Contains(filter)
                )
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
}
