using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryDeneme.Shelfs;
using LibraryDeneme.Permissions;
using LibraryDeneme.Books;
using Blazorise;
using Blazorise.DataGrid;
using Blazorise.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;


namespace LibraryDeneme.Blazor.Pages
{
    public partial class Shelfs
    {
        private IReadOnlyList<ShelfDto> ShelfList { get; set; }

        public int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;

        public int CurrentPage { get; set; }

        public string CurrentSorting { get; set; }

        public int TotalCount { get; set; }

        public bool CanCreateShelf { get; set; }

        public bool CanEditShelf { get; set; }

        public bool CanDeleteShelf { get; set; }

        public CreateShelfDto NewShelf { get; set; }

        public Guid EditingShelfId { get; set; }

        public UpdateShelfDto EditingShelf { get; set; }

        public Modal CreateShelfModal { get; set; }

        public Modal EditShelfModal { get; set; }

        public Validations CreateValidationsRef;

        public Validations EditValidationsRef;

        public Shelfs()
        {
            NewShelf = new CreateShelfDto();
            EditingShelf = new UpdateShelfDto();
        }
        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            await GetShelfsAsync();
        }

        public async Task SetPermissionsAsync()
        {
            CanCreateShelf = await AuthorizationService.IsGrantedAnyAsync(LibraryDenemePermissions.Shelfs.Create);

            CanEditShelf = await AuthorizationService.IsGrantedAnyAsync(LibraryDenemePermissions.Shelfs.Edit);

            CanDeleteShelf = await AuthorizationService.IsGrantedAnyAsync(LibraryDenemePermissions.Shelfs.Delete);

        }

        private async Task GetShelfsAsync()
        {
            var result = await ShelfAppService.GetListAsync(new GetShelfListDto
            {
                MaxResultCount = PageSize,
                SkipCount = CurrentPage * PageSize,
                Sorting = CurrentSorting

            }
            );

            ShelfList = result.Items;
            TotalCount = (int)result.TotalCount;

        }

        public async Task OnDataGridReadAsync(DataGridReadDataEventArgs<ShelfDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? "DESC" : ""))
                .JoinAsString(",");

            CurrentPage = e.Page - 1;
            await GetShelfsAsync();

            await InvokeAsync(StateHasChanged);
        }

        public void OpenCreateShelfModal()
        {
            CreateValidationsRef.ClearAll();

            NewShelf = new CreateShelfDto();
            CreateShelfModal.Show();
        }
        public void CloseCreateShelfModal()
        {

            CreateShelfModal.Hide();

        }
        public void OpenEditShelfModal(ShelfDto shelf)
        {
            EditValidationsRef.ClearAll();

            EditingShelfId = shelf.Id;
            EditingShelf = ObjectMapper.Map<ShelfDto, UpdateShelfDto>(shelf);
            EditShelfModal.Show();
        }

        public async Task DeleteShelfAsync(ShelfDto shelf)
        {
            var confirmMassage = L["ShelfDelectionConfirmationMassage", shelf.ShelfName];
            if (!await Message.Confirm(confirmMassage))
            {
                return;

            }
            await ShelfAppService.DeleteAsync(shelf.Id);
            await GetShelfsAsync();
        }

        public void CloseEditShelfModal()
        {
            EditShelfModal.Hide();
        }
        private async Task CreateShelfAsync()
        {
            if (await CreateValidationsRef.ValidateAll())
            {
                await ShelfAppService.CreateAsync(NewShelf);
                await GetShelfsAsync();
                CreateShelfModal.Hide();
            }
        }

        private async Task UpdateShelfAsync()
        {
            if (await EditValidationsRef.ValidateAll())
            {
                await ShelfAppService.UpdateAsync(EditingShelfId, EditingShelf);
                await GetShelfsAsync();
                EditShelfModal.Hide();
            }
        }



    }
}
