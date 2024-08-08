using LibraryDeneme.Books;
using Xunit;

namespace LibraryDeneme.EntityFrameworkCore.Applications.Books;

[Collection(LibraryDenemeTestConsts.CollectionDefinitionName)]
public class EfCoreBookAppService_Tests : BookAppService_Tests<LibraryDenemeEntityFrameworkCoreTestModule>
{

}