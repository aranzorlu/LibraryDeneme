using System;
using System.Threading.Tasks;
using LibraryDeneme.Authors;
using LibraryDeneme.Books;
using LibraryDeneme.Inventorys;
using LibraryDeneme.Shelfs;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace LibraryDeneme.Books;

public class LibraryDenemeDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
	private readonly IRepository<Inventory, Guid> _inventoryRepository;
	private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly AuthorManager _authorManager;
    private readonly ShelfManager _shelfManager;
    private readonly IShelfRepository _shelfRepository;
    private readonly BookManager _bookManager;


    public LibraryDenemeDataSeederContributor(
        IBookRepository bookRepository,
        BookManager bookManager,
        IShelfRepository shelfRepository,
        ShelfManager shelfManager,
        IAuthorRepository authorRepository,
        AuthorManager authorManager,
        IRepository<Inventory,Guid> inventoryRepository
        )
    {
        _inventoryRepository = inventoryRepository;
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _authorManager = authorManager;
        _shelfManager = shelfManager;
        _shelfRepository = shelfRepository;
        _bookManager = bookManager;
    }





    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _bookRepository.GetCountAsync() > 0)
        {
            return;
        }

        var a100 = await _shelfRepository.InsertAsync(
            await _shelfManager.CreateAsync(
                "a100",
                SType.Dystopia,
                BolumType.XKütüphanesi
            )
        );

        var a200 = await _shelfRepository.InsertAsync(
            await _shelfManager.CreateAsync(
                 "a200",
                 SType.ScienceFiction,
                 BolumType.XKütüphanesi
            )
        );

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

        var book1984 = await _bookRepository.InsertAsync(
            await _bookManager.CreateAsync(
                orwell.Id,
                "1984",
                BookType.Dystopia,
                new DateTime(1949, 6, 8))
            );

		var bookHitchhiker = await _bookRepository.InsertAsync(
			await _bookManager.CreateAsync(
				douglas.Id,
				"The Hitchhiker's Guide to the Galaxy",
				BookType.ScienceFiction,
				new DateTime(1995, 9, 27))
			);


        await _inventoryRepository.InsertAsync(
            new Inventory
            {
                ShelfId = a100.Id,
                BookId = book1984.Id,
                Floor = FloorNumber.Kat1,
                Bolum = BolumType.XKütüphanesi,
                BookStatu = StatuType.Inactive,
                SerialNo = "9789750718533"
               
            },
            autoSave: true
            );

		await _inventoryRepository.InsertAsync(
			new Inventory
			{
                ShelfId = a200.Id,
				BookId = bookHitchhiker.Id,
				Floor = FloorNumber.Kat2,
				Bolum = BolumType.YKütüphanesi,
                BookStatu = StatuType.Active,
				SerialNo = "9789750719387"

			},
			autoSave: true
			);



	}
}

