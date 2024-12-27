using DiiaNRCForm.Abstractions.Models;

namespace DiiaNRCForm.Abstractions.Services;

public interface IDiiaService
{
    Task<AuthDeepLinkModel> Authorization(string requestId);
}