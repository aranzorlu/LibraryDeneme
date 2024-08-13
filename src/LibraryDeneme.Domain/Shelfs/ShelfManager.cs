using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace LibraryDeneme.Shelfs;

public class ShelfManager : DomainService
{
    private readonly IShelfRepository _shelfRepository;

    public ShelfManager(IShelfRepository shelfRepository)
    {
        _shelfRepository = shelfRepository;
    }

    public async Task<Shelf> CreateAsync(
        string shelfname,
        SType shelftype)
        
    {
        Check.NotNullOrWhiteSpace(shelfname, nameof(shelfname));

        var existingShelf = await _shelfRepository.FindByNameAsync(shelfname);
        if (existingShelf != null)
        {
            throw new ShelfAlreadyExistsException(shelfname);
        }

        return new Shelf(
            GuidGenerator.Create(),
            shelfname,
            shelftype
            
        );
    }

    public async Task ChangeNameAsync(
        Shelf shelf,
        string newShelfName)
    {
        Check.NotNull(shelf, nameof(shelf));
        Check.NotNullOrWhiteSpace(newShelfName, nameof(newShelfName));

        var existingShelf = await _shelfRepository.FindByNameAsync(newShelfName);
        if (existingShelf != null && existingShelf.Id != shelf.Id)
        {
            throw new ShelfAlreadyExistsException(newShelfName);
        }

        shelf.ChangeName(newShelfName);
    }
}
