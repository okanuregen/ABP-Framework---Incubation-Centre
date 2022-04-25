using IsikUn.IncubationCentre.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace IsikUn.IncubationCentre.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(IncubationCentreEntityFrameworkCoreModule),
    typeof(IncubationCentreApplicationContractsModule)
    )]
public class IncubationCentreDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
