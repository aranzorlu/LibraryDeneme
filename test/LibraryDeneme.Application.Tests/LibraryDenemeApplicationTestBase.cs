using Volo.Abp.Modularity;

namespace LibraryDeneme;

public abstract class LibraryDenemeApplicationTestBase<TStartupModule> : LibraryDenemeTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
