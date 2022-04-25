using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IsikUn.IncubationCentre.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class IncubationCentreDbContextFactory : IDesignTimeDbContextFactory<IncubationCentreDbContext>
{
    public IncubationCentreDbContext CreateDbContext(string[] args)
    {
        IncubationCentreEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<IncubationCentreDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new IncubationCentreDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../IsikUn.IncubationCentre.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
