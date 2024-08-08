using Volo.Abp.Modularity;

namespace LibraryDeneme;

[DependsOn(
    typeof(LibraryDenemeDomainModule),
    typeof(LibraryDenemeTestBaseModule)
)]
public class LibraryDenemeDomainTestModule : AbpModule
{

}
