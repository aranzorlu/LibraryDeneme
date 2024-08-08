using LibraryDeneme.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace LibraryDeneme.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(LibraryDenemeEntityFrameworkCoreModule),
    typeof(LibraryDenemeApplicationContractsModule)
)]
public class LibraryDenemeDbMigratorModule : AbpModule
{
}
