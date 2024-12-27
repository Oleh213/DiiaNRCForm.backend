using Microsoft.AspNetCore.SignalR;

namespace DiiaNRCForm.Infrastructure.SignalR.Service;

public class SignatureHub: Hub<ISignatureHubClient>
{
    public const string HubDirection = "/hubs/signatureHub";
    
    public async Task AddToGroupAsync(Guid requestId)
    {
        Console.WriteLine("AddToGroup: " + requestId);
        await Groups.AddToGroupAsync(Context.ConnectionId, requestId.ToString());
    }
}
