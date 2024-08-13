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
        IRepository<Shelf, Guid> shelfRepository,
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

        var orwell = await _authorRepository.InsertAsync(
            await _authorManager.CreateAsync(
                "George Orwell",
                new DateTime(1903, 06, 25),
                "Orwell produced literary criticism and poetry, fiction and polemical journalism; and is best known for the allegorical novella Animal Farm (1945) and the dystopian novel Nineteen Eighty-Four (1949)."
            )
        );

        var douglas = await _authorRepository.InsertAsync(
            await _authorManager.CreateAsync(
                "Douglas Adams",
                new DateTime(1952, 03, 11),
                "Douglas Adams was an English author, screenwriter, essayist, humorist, satirist and dramatist. Adams was an advocate for environmentalism and conservation, a lover of fast cars, technological innovation and the Apple Macintosh, and a self-proclaimed 'radical atheist'."
            )
        );

        await _bookRepository.InsertAsync(
            new Book
            {
                AuthorId = orwell.Id, // SET THE AUTHOR
                Name = "1984",
                Type = BookType.Dystopia,
                PublishDate = new DateTime(1949, 6, 8),
                Price = 19.84f
            },
            autoSave: true
        );

        await _bookRepository.InsertAsync(
            new Book
            {
                AuthorId = douglas.Id, // SET THE AUTHOR
                Name = "The Hitchhiker's Guide to the Galaxy",
                Type = BookType.ScienceFiction,
                PublishDate = new DateTime(1995, 9, 27),
                Price = 42.0f
            },
            autoSave: true
        );
    await _shelfRepository.InsertAsync(
           new Shelf
           {

               ShelfName = "Sciencefiction1",
               ShelfType = SType.ScienceFiction,

           },
           autoSave: true);


    }
}

