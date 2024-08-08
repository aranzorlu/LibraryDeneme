using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace LibraryDeneme.Data;

/* This is used if database provider does't define
 * ILibraryDenemeDbSchemaMigrator implementation.
 */
public class NullLibraryDenemeDbSchemaMigrator : ILibraryDenemeDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
