using IsikUn.IncubationCentre.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace IsikUn.IncubationCentre.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class IncubationCentrePageModel : AbpPageModel
{
    protected IncubationCentrePageModel()
    {
        LocalizationResourceType = typeof(IncubationCentreResource);
    }
}
