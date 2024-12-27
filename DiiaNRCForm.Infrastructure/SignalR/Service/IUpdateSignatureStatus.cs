namespace DiiaNRCForm.Infrastructure.SignalR.Service;

public interface IUpdateSignatureStatus
{
    Task UpdateSignatureStatusAsync(Guid requestId);
}