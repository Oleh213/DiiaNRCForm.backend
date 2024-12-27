using System.Text.Json;
using DiiaClient.SDK.Interfaces;
using DiiaClient.SDK.Models.Remote;
using DiiaNRCForm.Abstractions.Interfaces;
using DiiaNRCForm.Abstractions.Models;
using DiiaNRCForm.Abstractions.Services;

namespace DiiaNRCForm.DiiaService;

public class DiiaService : IDiiaService
{
    private static IDiia _diiaClient;
    private readonly IQRCreator _qrCreator;
    public DiiaService(
        IDiia diia,
        IQRCreator qrCreator)
    {
        _diiaClient = diia;
        _qrCreator = qrCreator;
    }
    
    public async Task<AuthDeepLinkModel> Authorization(string requestId)
    {
        var branchId = await CreateBranchAsync();
        
        var authDeepLink = await _diiaClient.GetAuthDeepLink(branchId, await CreateOfferAsync(branchId), requestId, $"https://ee.kobotoolbox.org/single/2KFJZxX3?d[signatureId]={requestId}");
        
        return new AuthDeepLinkModel
        {
            DeepLink = authDeepLink.DeepLink,
            Id = authDeepLink.RequestId, 
            HashedId = authDeepLink.RequestIdHash,
            QrCode = _qrCreator.GenerateQRCode(authDeepLink.DeepLink), 
            RedirectLink = $"https://ee.kobotoolbox.org/single/2KFJZxX3?d[signatureId]={requestId}"
        };
    }
    
    private async Task<string> CreateBranchAsync()
    {
        var str =
            "{\"customFullName\":\"Norwegian Refugee Council\", \"customFullAddress\":\"м. Київ, вул. Ніжинська, 29Б\"," +
            "\"name\":\"Name\", \"email\":\"vladislav.kharlamov@alfabank.kiev.ua\", \"region\":\"Київська обл.\"," +
            "\"district\":\"Києво-Святошинський р-н\", \"location\":\"м. Київ\", \"street\":\"вул. Ніжинська\"," +
            "\"house\":\"29Д\", \"deliveryTypes\": [\"api\"], \"offerRequestType\": \"dynamic\"," +
            "\"scopes\":{\"diiaId\":[\"auth\"]}}";
        
        Branch branch = JsonSerializer.Deserialize<Branch>(str);

        Branch branchList = await _diiaClient.CreateBranch(branch);
        
        return branchList.Id;
    }
    
    private async Task<string> CreateOfferAsync(string branchId)
    {
        var offerAuth = new Offer()
        {
            Name = "Test Auth",
            ReturnLink = "https://google.com",
            Scopes = new OfferScopes()
            {
                DiiaId = new List<string>() { "auth" }
            }
        }; 
        
        return await _diiaClient.CreateOffer(branchId, offerAuth);
    }
}