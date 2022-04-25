using IsikUn.IncubationCentre.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace IsikUn.IncubationCentre.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class IncubationCentreController : AbpControllerBase
{
    protected IncubationCentreController()
    {
        LocalizationResource = typeof(IncubationCentreResource);
    }
}
