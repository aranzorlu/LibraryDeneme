using System;
using System.Threading.Tasks;
using LibraryDeneme.Authors;
using LibraryDeneme.Books;
using LibraryDeneme.Shelfs;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace LibraryDeneme.Books;

public class LibraryDenemeDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Book, Guid> _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly AuthorManager _authorManager;
    private readonly IRepository<Shelf, Guid> _shelfRepository;

    public LibraryDenemeDataSeederContributor(
        IRepository<Book, Guid> bookRepository,
        IRepository<Shelf,Guid> shelfRepository,
        IAuthorRepository authorRepository,
        AuthorManager authorManager)
    {
        _shelfRepository = shelfRepository;
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _authorManager = authorManager;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _bookRepository.GetCountAsync() > 0)
        {

            return;
        }


          

            
            
            await _shelfRepository.InsertAsync(
            new Shelf
            {

                ShelfName = "Sciencefiction1",
                ShelfType = SType.ScienceFiction,

            },
            autoSave: true);


    }
}
