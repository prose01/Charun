using Charun.Data;
using Charun.Helpers;
using Charun.Interfaces;
using Charun.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureAppConfiguration(configBuilder => configBuilder.AddEnvironmentVariables().AddJsonFile("appsettings.json", false, true))
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddOptions<Settings>().Configure<IConfiguration>((settings, configuration) => configuration.GetSection("Configs").Bind(settings));
        services.AddSingleton<Context, Context>();
        services.AddSingleton<IAzureBlobStorage, AzureBlobStorage>();
        services.AddSingleton<IProfilesQueryRepository, ProfilesQueryRepository>();
        services.AddSingleton<IFeedbackRepository, FeedbackRepository>();
        services.AddSingleton<IJunoRepository, JunoRepository>();
        services.AddSingleton<IHelperMethods, HelperMethods>();
    })
    .Build();

host.Run();
