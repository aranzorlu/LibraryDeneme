﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryDeneme.Authors;
using LibraryDeneme.Permissions;
using LibraryDeneme.Books;
using Blazorise;
using Blazorise.DataGrid;
using Blazorise.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;


namespace LibraryDeneme.Blazor.Pages
{
    public partial class Authors
    {
        private IReadOnlyList<AuthorDto> AuthorList { get; set; }

        public int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;

        public int CurrentPage { get; set; }

        public string CurrentSorting { get; set; }

        public int TotalCount { get; set; }

        public bool CanCreateAuthor { get; set; }

        public bool CanEditAuthor { get; set; }

        public bool CanDeleteAuthor { get; set; }

        public CreateAuthorDto NewAuthor { get; set; }

        public Guid EditingAuthorId { get; set; }

        public UpdateAuthorDto EditingAuthor { get; set; }

        public Modal CreateAuthorModal { get; set; }

        public Modal EditAuthorModal { get; set; }

        public Validations CreateValidationsRef;

        public Validations EditValidationsRef;

        public Authors()
        {
            NewAuthor = new CreateAuthorDto();
            EditingAuthor = new UpdateAuthorDto();
        }
        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            await GetAuthorsAsync();
        }

        public async Task SetPermissionsAsync()
        {
            CanCreateAuthor = await AuthorizationService.IsGrantedAnyAsync(LibraryDenemePermissions.Authors.Create);

            CanEditAuthor = await AuthorizationService.IsGrantedAnyAsync(LibraryDenemePermissions.Authors.Edit);

            CanDeleteAuthor = await AuthorizationService.IsGrantedAnyAsync(LibraryDenemePermissions.Authors.Delete);

        }

        private async Task GetAuthorsAsync()
        {
            var result = await AuthorAppService.GetListAsync(new GetAuthorListDto
            {
                MaxResultCount = PageSize,
                SkipCount = CurrentPage * PageSize,
                Sorting = CurrentSorting

            }
            );

            AuthorList = result.Items;
            TotalCount = (int)result.TotalCount;

        }

        public async Task OnDataGridReadAsync(DataGridReadDataEventArgs<AuthorDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? "DESC" : ""))
                .JoinAsString(",");

            CurrentPage = e.Page - 1;
            await GetAuthorsAsync();

            await InvokeAsync(StateHasChanged);
        }

        public void OpenCreateAuthorModal()
        {
            CreateValidationsRef.ClearAll();

            NewAuthor = new CreateAuthorDto();
            CreateAuthorModal.Show();
        }
        public void CloseCreateAuthorModal()
        {

            CreateAuthorModal.Hide();

        }
        public void OpenEditAuthorModal(AuthorDto author)
        {
            EditValidationsRef.ClearAll();

            EditingAuthorId = author.Id;
            EditingAuthor = ObjectMapper.Map<AuthorDto, UpdateAuthorDto>(author);
            EditAuthorModal.Show();
        }

        public async Task DeleteAuthorAsync(AuthorDto author)
        {
            var confirmMassage = L["AuthorDelectionConfirmationMassage", author.Name];
            if (!await Message.Confirm(confirmMassage))
            {
                return;

            }
            await AuthorAppService.DeleteAsync(author.Id);
            await GetAuthorsAsync();
        }

        public void CloseEditAuthorModal()
        {
            EditAuthorModal.Hide();
        }
        private async Task CreateAuthorAsync()
        {
            if (await CreateValidationsRef.ValidateAll())
            {
                await AuthorAppService.CreateAsync(NewAuthor);
                await GetAuthorsAsync();
                CreateAuthorModal.Hide();
            }
        }

        private async Task UpdateAuthorAsync()
        {
            if (await EditValidationsRef.ValidateAll())
            {
                await AuthorAppService.UpdateAsync(EditingAuthorId, EditingAuthor);
                await GetAuthorsAsync();
                EditAuthorModal.Hide();
            }
        }



    }
}
