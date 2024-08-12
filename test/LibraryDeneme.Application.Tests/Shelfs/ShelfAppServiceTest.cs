using System;
using System.Linq;
using System.Threading.Tasks;
using LibraryDeneme.Books;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Xunit;

namespace LibraryDeneme.Shelfs;

public abstract class ShelfAppService_Tests<TStartupModule> : LibraryDenemeApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IShelfAppService _shelfAppService;

    protected ShelfAppService_Tests()
    {
        _shelfAppService = GetRequiredService<IShelfAppService>();
    }

   

    [Fact]
    public async Task Should_Create_A_Valid_Shelf()
    {
        //Act
        var result = await _shelfAppService.CreateAsync(
            new CreateUpdateShelfDto
            {
                ShelfName = "New test book 42",
               
                ShelfType = SType.ScienceFiction
            }
        );

        //Assert
        result.Id.ShouldNotBe(Guid.Empty);
        result.ShelfName.ShouldBe("New test book 42");
    }
    [Fact]
    public async Task Should_Not_Create_A_Shelf_Without_Name()
    {
        var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
        {
            await _shelfAppService.CreateAsync(
                new CreateUpdateShelfDto
                {
                    ShelfName = "",
                   
                    ShelfType = SType.ScienceFiction
                }
            );
        });

        exception.ValidationErrors
            .ShouldContain(err => err.MemberNames.Any(mem => mem == "ShelfName"));
    }

}
