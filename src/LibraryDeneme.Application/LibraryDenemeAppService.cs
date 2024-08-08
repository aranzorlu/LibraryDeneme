using LibraryDeneme.Localization;
using Volo.Abp.Application.Services;

namespace LibraryDeneme;

/* Inherit your application services from this class.
 */
public abstract class LibraryDenemeAppService : ApplicationService
{
    protected LibraryDenemeAppService()
    {
        LocalizationResource = typeof(LibraryDenemeResource);
    }
}
