using Microsoft.AspNetCore.SignalR;

namespace DiiaNRCForm.Infrastructure.SignalR.Service;

public class UpdateSignatureStatus : IUpdateSignatureStatus
{
    private readonly IHubContext<SignatureHub, ISignatureHubClient> _signatureHub;

    public UpdateSignatureStatus(IHubContext<SignatureHub, ISignatureHubClient> signatureHub)
    {
        _signatureHub = signatureHub;
    }
    
    public async Task UpdateSignatureStatusAsync(Guid requestId)
    {
        await _signatureHub.Clients.Group(requestId.ToString()).ReceiveSignatureStatusEventOnClient(requestId).ConfigureAwait(false);
    }
}