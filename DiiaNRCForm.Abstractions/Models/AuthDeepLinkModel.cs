namespace DiiaNRCForm.Abstractions.Models;

public class AuthDeepLinkModel
{
    public string DeepLink { get; set; }
    public string HashedId { get; set; }
    public string Id { get; set; }
    public string QrCode { get; set; }
    public string RedirectLink { get; set; }
}