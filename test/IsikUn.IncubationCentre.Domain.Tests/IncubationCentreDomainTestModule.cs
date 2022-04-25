using IsikUn.IncubationCentre.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace IsikUn.IncubationCentre;

[DependsOn(
    typeof(IncubationCentreEntityFrameworkCoreTestModule)
    )]
public class IncubationCentreDomainTestModule : AbpModule
{

}
