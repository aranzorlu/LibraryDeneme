using LibraryDeneme.Samples;
using Xunit;

namespace LibraryDeneme.EntityFrameworkCore.Applications;

[Collection(LibraryDenemeTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<LibraryDenemeEntityFrameworkCoreTestModule>
{

}
