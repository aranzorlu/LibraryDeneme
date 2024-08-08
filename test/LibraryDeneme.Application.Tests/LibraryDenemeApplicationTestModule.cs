using Volo.Abp.Modularity;

namespace LibraryDeneme;

[DependsOn(
    typeof(LibraryDenemeApplicationModule),
    typeof(LibraryDenemeDomainTestModule)
)]
public class LibraryDenemeApplicationTestModule : AbpModule
{

}
