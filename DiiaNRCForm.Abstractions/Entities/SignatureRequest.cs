namespace DiiaNRCForm.Abstractions.Entities;

public class SignatureRequest : Entity
{
    public bool Signed { get; set; }
    public string HashedId { get; set; }
}