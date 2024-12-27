using DiiaNRCForm.Infrastructure.SignalR.Service;
using Microsoft.Extensions.DependencyInjection;

namespace DiiaNRCForm.Infrastructure.SignalR;

public static class SignalRServiceCollectionExtensions
{ 
    public static void AddSignalRInfrastructure(this IServiceCollection services)
    {
        services.AddSignalR(e => e.MaximumReceiveMessageSize = 102400000);
        services.AddSingleton<IUpdateSignatureStatus, UpdateSignatureStatus>();
    }
}


