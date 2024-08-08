using LibraryDeneme.Samples;
using Xunit;

namespace LibraryDeneme.EntityFrameworkCore.Domains;

[Collection(LibraryDenemeTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<LibraryDenemeEntityFrameworkCoreTestModule>
{

}
