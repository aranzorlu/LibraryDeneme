using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryDeneme.Permissions;
using LibraryDeneme.Books;
using Blazorise;
using Blazorise.DataGrid;
using Blazorise.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;


namespace LibraryDeneme.Blazor.Pages
{
    public partial class Books
    {
        private IReadOnlyList<BookDto> BookList { get; set; }

        public int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;

        public int CurrentPage { get; set; }

        public string CurrentSorting { get; set; }

        public int TotalCount { get; set; }

        public bool CanCreateBook { get; set; }

        public bool CanEditBook { get; set; }

        public bool CanDeleteBook { get; set; }

        public CreateBookDto NewBook { get; set; }

        public Guid EditingBookId { get; set; }

        public UpdateBookDto EditingBook { get; set; }

        public Modal CreateBookModal { get; set; }

        public Modal EditBookModal { get; set; }

        public Validations CreateValidationsRef;


        public Validations EditValidationsRef;

        public Books()
        {
            NewBook = new CreateBookDto();
            EditingBook = new UpdateBookDto();
        }
        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            await GetBooksAsync();
            await base.OnInitializedAsync();
            authorList = (await BookAppService.GetAuthorLookupAsync()).Items;

            await LoadBooksAsync();
        }

        public async Task SetPermissionsAsync()
        {
            CanCreateBook = await AuthorizationService.IsGrantedAnyAsync(LibraryDenemePermissions.Books.Create);

            CanEditBook = await AuthorizationService.IsGrantedAnyAsync(LibraryDenemePermissions.Books.Edit);

            CanDeleteBook = await AuthorizationService.IsGrantedAnyAsync(LibraryDenemePermissions.Books.Delete);

        }

        private async Task GetBooksAsync()
        {
            var result = await BookAppService.GetListAsync(new GetBookListDto
            {
                MaxResultCount = PageSize,
                SkipCount = CurrentPage * PageSize,
                Sorting = CurrentSorting

            }
            );

            BookList = result.Items;
            TotalCount = (int)result.TotalCount;

        }

        public async Task OnDataGridReadAsync(DataGridReadDataEventArgs<BookDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? "DESC" : ""))
                .JoinAsString(",");

            CurrentPage = e.Page - 1;
            await GetBooksAsync();

            await InvokeAsync(StateHasChanged);
        }

        public void OpenCreateBookModal()
        {
            CreateValidationsRef.ClearAll();

            NewBook = new CreateBookDto();
            CreateBookModal.Show();
        }
        public void CloseCreateBookModal()
        {

            CreateBookModal.Hide();

        }
        public void OpenEditBookModal(BookDto book)
        {
            EditValidationsRef.ClearAll();

            EditingBookId = book.Id;
            EditingBook = ObjectMapper.Map<BookDto, UpdateBookDto>(book);
            EditBookModal.Show();
        }

        public async Task DeleteBookAsync(BookDto book)
        {
            var confirmMassage = L["AuthorDelectionConfirmationMassage", book.Name];
            if (!await Message.Confirm(confirmMassage))
            {
                return;

            }
            await BookAppService.DeleteAsync(book.Id);
            await GetBooksAsync();
        }

        public void CloseEditBookModal()
        {
            EditBookModal.Hide();
        }
        private async Task CreateBookAsync()
        {
            if (await CreateValidationsRef.ValidateAll())
            {
                await BookAppService.CreateAsync(NewBook);
                await GetBooksAsync();
                CreateBookModal.Hide();
            }
        }

        private async Task UpdateBookAsync()
        {
            if (await EditValidationsRef.ValidateAll())
            {
                await BookAppService.UpdateAsync(EditingBookId, EditingBook);
                await GetBooksAsync();
                EditBookModal.Hide();
            }
        }



    }
}
