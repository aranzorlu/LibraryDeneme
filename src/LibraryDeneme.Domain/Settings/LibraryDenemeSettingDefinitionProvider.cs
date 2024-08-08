using Volo.Abp.Settings;

namespace LibraryDeneme.Settings;

public class LibraryDenemeSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(LibraryDenemeSettings.MySetting1));
    }
}
