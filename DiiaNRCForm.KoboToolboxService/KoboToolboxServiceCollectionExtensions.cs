using DiiaNRCForm.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DiiaNRCForm.KoboToolboxService;

public static class KoboToolboxServiceCollectionExtensions
{
    public static void AddKoboToolboxServiceInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IKoboToolboxService, KoboToolboxService>();
    }
}