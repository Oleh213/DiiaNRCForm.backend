using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using DiiaNRCForm.Abstractions.AppSettings;
using DiiaNRCForm.Abstractions.Services;

namespace DiiaNRCForm.KoboToolboxService;

public class KoboToolboxService : IKoboToolboxService
{

    private readonly AppSettings _appSettings;
    private static string _koboEditFormUrl;

    public KoboToolboxService(AppSettings appSettings)
    {
        _appSettings = appSettings;
        _koboEditFormUrl = $"https://kf.kobotoolbox.org/api/v2/assets/{appSettings.KoboToolboxSettings.KoboFormId}/data/bulk/";
    }

    public async Task UpdateSignatureStatus(int submissionId)
    {
        var payload = new
        {
            payload = new
            {
                submission_ids = new[] { submissionId },
                data = new
                {
                    Signed = "Yes"
                },
                confirm = true
            }
        };
        
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", _appSettings.KoboToolboxSettings.KoboApiKey);
        
            var urlWithParams = $"{_koboEditFormUrl}?fomat=json"; 
        
            string jsonPayload = JsonSerializer.Serialize(payload);
        
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PatchAsync(urlWithParams, content);
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("KoboToolbox API call failed");
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}