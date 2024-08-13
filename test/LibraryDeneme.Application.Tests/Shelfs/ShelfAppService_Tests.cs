using System;
using System.Threading.Tasks;

using Shouldly;
using Volo.Abp.Modularity;
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
    public async Task Should_Get_All_Shelfs_Without_Any_Filter()
    {
        var result = await _shelfAppService.GetListAsync(new GetShelfListDto());

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
        result.Items.ShouldContain(shelf => shelf.ShelfName == "a100");
        result.Items.ShouldContain(shelf => shelf.ShelfName == "a200");
    }

    [Fact]
    public async Task Should_Get_Filtered_Shelfs()
    {
        var result = await _shelfAppService.GetListAsync(
            new GetShelfListDto { Filter = "100" });

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        result.Items.ShouldContain(shelf => shelf.ShelfName == "a100");
        result.Items.ShouldNotContain(shelf => shelf.ShelfName == "a200");
    }

    [Fact]
    public async Task Should_Create_A_New_Shelf()
    {
        var shelfDto = await _shelfAppService.CreateAsync(
            new CreateShelfDto
            {
                ShelfName = "a300",
                ShelfType = SType.Fantastic,
               
            }
        );

        shelfDto.Id.ShouldNotBe(Guid.Empty);
        shelfDto.ShelfName.ShouldBe("a300");
    }

    [Fact]
    public async Task Should_Not_Allow_To_Create_Duplicate_Shelf()
    {
        await Assert.ThrowsAsync<ShelfAlreadyExistsException>(async () =>
        {
            await _shelfAppService.CreateAsync(
                new CreateShelfDto
                {
                    ShelfName = "a500",
                    ShelfType = SType.Science,
                    
                }
            );
        });
    }

    //TODO: Test other methods...
}
