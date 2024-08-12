using LibraryDeneme.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace LibraryDeneme.Permissions;

public class LibraryDenemePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(LibraryDenemePermissions.GroupName);

        var booksPermission = myGroup.AddPermission(LibraryDenemePermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(LibraryDenemePermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(LibraryDenemePermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(LibraryDenemePermissions.Books.Delete, L("Permission:Books.Delete"));

        var authorsPermission = myGroup.AddPermission(
    LibraryDenemePermissions.Authors.Default, L("Permission:Authors"));
        authorsPermission.AddChild(
            LibraryDenemePermissions.Authors.Create, L("Permission:Authors.Create"));
        authorsPermission.AddChild(
            LibraryDenemePermissions.Authors.Edit, L("Permission:Authors.Edit"));
        authorsPermission.AddChild(
            LibraryDenemePermissions.Authors.Delete, L("Permission:Authors.Delete"));

        
        var shelfsPermission = myGroup.AddPermission(LibraryDenemePermissions.Shelfs.Default, L("Permission:Shelfs"));
        shelfsPermission.AddChild(LibraryDenemePermissions.Shelfs.Create, L("Permission:Shelfs.Create"));
        shelfsPermission.AddChild(LibraryDenemePermissions.Shelfs.Edit, L("Permission:Shelfs.Edit"));
        shelfsPermission.AddChild(LibraryDenemePermissions.Shelfs.Delete, L("Permission:Shelfs.Delete"));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(LibraryDenemePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<LibraryDenemeResource>(name);
    }
}
