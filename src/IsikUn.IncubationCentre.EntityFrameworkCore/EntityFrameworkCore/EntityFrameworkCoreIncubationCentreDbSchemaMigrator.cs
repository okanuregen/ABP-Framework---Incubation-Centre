using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IsikUn.IncubationCentre.Data;
using Volo.Abp.DependencyInjection;

namespace IsikUn.IncubationCentre.EntityFrameworkCore;

public class EntityFrameworkCoreIncubationCentreDbSchemaMigrator
    : IIncubationCentreDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreIncubationCentreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the IncubationCentreDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<IncubationCentreDbContext>()
            .Database
            .MigrateAsync();
    }
}
