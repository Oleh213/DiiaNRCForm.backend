namespace DiiaNRCForm.Infrastructure.SignalR.Service;

public interface ISignatureHubClient
{
    Task AddToGroupAsync(Guid requestId);
    Task ReceiveSignatureStatusEventOnClient(Guid requestId);
}