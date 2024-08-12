
using LibraryDeneme.Shelfs;
using Xunit;

namespace LibraryDeneme.EntityFrameworkCore.Applications.Shelfs;

[Collection(LibraryDenemeTestConsts.CollectionDefinitionName)]
public class EfCoreShelfAppService_Tests : ShelfAppService_Tests<LibraryDenemeEntityFrameworkCoreTestModule>
{

}
