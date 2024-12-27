namespace DiiaNRCForm.Abstractions.Services;

public interface IKoboToolboxService
{
    Task UpdateSignatureStatus(int submissionId);
}