using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace IsikUn.IncubationCentre.Web;

[Dependency(ReplaceServices = true)]
public class IncubationCentreBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "IncubationCentre";
    public override string LogoUrl => "img/logo.png";

}
