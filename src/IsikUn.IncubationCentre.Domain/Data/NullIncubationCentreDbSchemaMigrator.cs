using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace IsikUn.IncubationCentre.Data;

/* This is used if database provider does't define
 * IIncubationCentreDbSchemaMigrator implementation.
 */
public class NullIncubationCentreDbSchemaMigrator : IIncubationCentreDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
