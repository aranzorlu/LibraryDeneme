
using LibraryDeneme.Shelfs;
using Xunit;

namespace LibraryDeneme.EntityFrameworkCore.Applications.Authors;

[Collection(LibraryDenemeTestConsts.CollectionDefinitionName)]
public class EfCoreShelfAppService_Tests : ShelfAppService_Tests<LibraryDenemeEntityFrameworkCoreTestModule>
{

}
