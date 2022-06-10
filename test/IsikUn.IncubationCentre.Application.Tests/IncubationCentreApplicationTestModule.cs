using Volo.Abp.Modularity;


namespace IsikUn.IncubationCentre;

[DependsOn(
    typeof(IncubationCentreApplicationModule),
    typeof(IncubationCentreDomainTestModule)
    )]
public class IncubationCentreApplicationTestModule : AbpModule
{
    
   
}
