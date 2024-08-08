using Xunit;

namespace LibraryDeneme.EntityFrameworkCore;

[CollectionDefinition(LibraryDenemeTestConsts.CollectionDefinitionName)]
public class LibraryDenemeEntityFrameworkCoreCollection : ICollectionFixture<LibraryDenemeEntityFrameworkCoreFixture>
{

}
