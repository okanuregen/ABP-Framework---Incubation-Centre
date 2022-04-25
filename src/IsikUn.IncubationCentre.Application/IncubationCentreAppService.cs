using System;
using System.Collections.Generic;
using System.Text;
using IsikUn.IncubationCentre.Localization;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre;

/* Inherit your application services from this class.
 */
public abstract class IncubationCentreAppService : ApplicationService
{
    protected IncubationCentreAppService()
    {
        LocalizationResource = typeof(IncubationCentreResource);
    }
}
