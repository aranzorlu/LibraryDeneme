using LibraryDeneme.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Volo.Abp;

namespace LibraryDeneme.Books;

    public class BookManager : DomainService
    {
        private readonly IBookRepository _bookRepository;

        public BookManager(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> CreateAsync(
            Guid authorId,
            string name,
            BookType type,
            DateTime publishDate)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingBook = await _bookRepository.FindByNameAsync(name);
            if (existingBook != null)
            {
                throw new BookAlreadyExistsException(name);
            }

            return new Book(
                GuidGenerator.Create(),
                authorId,
                name,
                type,
                publishDate
            );
        }

        public async Task ChangeNameAsync(
            Book book,
            string newName)
        {
            Check.NotNull(book, nameof(book));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingbook = await _bookRepository.FindByNameAsync(newName);
            if (existingbook != null && existingbook.Id != book.Id)
            {
                throw new BookAlreadyExistsException(newName);
            }

            book.ChangeName(newName);
        }
    }
