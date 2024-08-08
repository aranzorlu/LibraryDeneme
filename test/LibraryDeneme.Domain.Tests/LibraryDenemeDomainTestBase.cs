using Volo.Abp.Modularity;

namespace LibraryDeneme;

/* Inherit from this class for your domain layer tests. */
public abstract class LibraryDenemeDomainTestBase<TStartupModule> : LibraryDenemeTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
