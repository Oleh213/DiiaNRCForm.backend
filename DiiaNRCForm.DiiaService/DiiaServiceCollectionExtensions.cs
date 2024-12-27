using DiiaClient.CryptoAPI;
using DiiaClient.SDK;
using DiiaClient.SDK.Interfaces;
using DiiaNRCForm.Abstractions.AppSettings;
using DiiaNRCForm.Abstractions.Interfaces;
using DiiaNRCForm.Abstractions.Services;
using DiiaNRCForm.DiiaService.QRGenerator;
using Microsoft.Extensions.DependencyInjection;

namespace DiiaNRCForm.DiiaService;

public static class DiiaServiceCollectionExtensions
{
    public static void AddDiiaServiceInfrastructure(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddHttpClient();
        
        services.AddScoped<IDiia>(provider => new Diia(
            appSettings.DiiaSettings.AcquirerToken,
            appSettings.DiiaSettings.AuthAcquirerToken,
            appSettings.DiiaSettings.DiiaHost,
            provider.GetService<ICryptoService>()));
        
        services.AddScoped<IDiiaService, DiiaService>();
        services.AddScoped<IQRCreator, QRCreator>();
    }
}
