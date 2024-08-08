using LibraryDeneme.Localization;
using Volo.Abp.AspNetCore.Components;

namespace LibraryDeneme.Blazor;

public abstract class LibraryDenemeComponentBase : AbpComponentBase
{
    protected LibraryDenemeComponentBase()
    {
        LocalizationResource = typeof(LibraryDenemeResource);
    }
}
