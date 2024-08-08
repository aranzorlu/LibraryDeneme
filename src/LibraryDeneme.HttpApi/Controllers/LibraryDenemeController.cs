﻿using LibraryDeneme.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace LibraryDeneme.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class LibraryDenemeController : AbpControllerBase
{
    protected LibraryDenemeController()
    {
        LocalizationResource = typeof(LibraryDenemeResource);
    }
}
