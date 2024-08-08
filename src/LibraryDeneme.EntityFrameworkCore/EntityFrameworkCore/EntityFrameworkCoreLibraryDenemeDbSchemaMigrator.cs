using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LibraryDeneme.Data;
using Volo.Abp.DependencyInjection;

namespace LibraryDeneme.EntityFrameworkCore;

public class EntityFrameworkCoreLibraryDenemeDbSchemaMigrator
    : ILibraryDenemeDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreLibraryDenemeDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the LibraryDenemeDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<LibraryDenemeDbContext>()
            .Database
            .MigrateAsync();
    }
}
