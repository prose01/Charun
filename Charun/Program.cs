using Charun.Data;
using Charun.Helpers;
using Charun.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddSingleton<IAzureBlobStorage, AzureBlobStorage>();
        services.AddSingleton<IProfilesQueryRepository, ProfilesQueryRepository>();
        services.AddSingleton<IFeedbackRepository, FeedbackRepository>();
        services.AddSingleton<IJunoRepository, JunoRepository>();
        services.AddSingleton<IHelperMethods, HelperMethods>();
    })
    .Build();

host.Run();
