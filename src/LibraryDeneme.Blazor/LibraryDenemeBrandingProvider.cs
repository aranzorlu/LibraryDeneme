using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace LibraryDeneme.Blazor;

[Dependency(ReplaceServices = true)]
public class LibraryDenemeBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Kütüphane Uygulaması";
}
