﻿using System.Threading.Tasks;
using LibraryDeneme.Localization;
using LibraryDeneme.Permissions;
using LibraryDeneme.MultiTenancy;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.Identity.Blazor;
using LibraryDeneme.Authors;
using NUglify.JavaScript.Syntax;
using LibraryDeneme.Books;
using Blazorise;

namespace LibraryDeneme.Blazor.Menus;

public class LibraryDenemeMenuContributor : IMenuContributor
{
    

	public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<LibraryDenemeResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                LibraryDenemeMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home",
                order: 1
            )
        );

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 4;

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        context.Menu.AddItem(
         new ApplicationMenuItem(
             "BooksStore",
             l["Menu:LibraryDeneme"],
             icon: "fas fa-building",
             url:"/buildings"
            ).AddItem(
                new ApplicationMenuItem(
                "BooksStore.Books",
                l["Menu:XLibrary"]

                
                )
                .AddItem(
                new ApplicationMenuItem(
                "BooksStore.Books.Book",
                l["Menu:Books"],
                url: "/xbooks"
                )
                )
                

                .AddItem(new ApplicationMenuItem(
                   "BookStore.Books.Shelfs",
                    l["Menu:Shelfs"],
                    url: "/xshelfs")
                )
                
                 
            )
            .AddItem(
             new ApplicationMenuItem(
                 "Bookstore.Books",
                 l["Menu:YLibrary"]

                 )
             .AddItem(new ApplicationMenuItem(
                 "Bookstore.Books.Book",
                 l["Menu:YBooks"],
                 url: "/ybooks")
             
             )
             .AddItem(new ApplicationMenuItem(
                 "Booksotre.Books.Book",
                 l["Menu:YShelfs"],
                 url:"/yshelfs"
                 )
             )
             
             )




        );
         
        
        

        if (await context.IsGrantedAsync(LibraryDenemePermissions.Authors.Default))
        {
            context.Menu.AddItem(new ApplicationMenuItem(
                "BookStore.Authors",
                l["Menu:Authors"],
                url: "/authors",
                icon: "fas fa-user")

                );
        }
        if (await context.IsGrantedAsync(LibraryDenemePermissions.Shelfs.Default))
        {
            context.Menu.AddItem(new ApplicationMenuItem(
                "BookStore.Shelfs",
                l["Menu:Shelfs"],
                url: "/shelfs",
                icon:"fas fa-archive")
                );
        }
        if (await context.IsGrantedAsync(LibraryDenemePermissions.Books.Default))
        {
            context.Menu.AddItem(new ApplicationMenuItem(
                "BookStore.Books",
                l["Menu:Books"],
                url: "/books",
                icon:"fas fa-book")
                );
        }
        context.Menu.AddItem(new ApplicationMenuItem(
                "BookStore.Inventory",
                l["Menu:Inventory"],
                url: "/inventorys",
                icon:"fas fa-box"
                )
                );

    }

}
