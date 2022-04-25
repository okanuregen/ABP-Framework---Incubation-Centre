using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace IsikUn.IncubationCentre;

public class IncubationCentreWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<IncubationCentreWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
