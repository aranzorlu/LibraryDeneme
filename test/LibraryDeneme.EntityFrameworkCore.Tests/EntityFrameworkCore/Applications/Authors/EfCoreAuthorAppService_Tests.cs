using Acme.BookStore.Authors;
using LibraryDeneme.Authors;
using Xunit;

namespace LibraryDeneme.EntityFrameworkCore.Applications.Authors;

[Collection(LibraryDenemeTestConsts.CollectionDefinitionName)]
public class EfCoreAuthorAppService_Tests : AuthorAppService_Tests<LibraryDenemeEntityFrameworkCoreTestModule>
{

}
