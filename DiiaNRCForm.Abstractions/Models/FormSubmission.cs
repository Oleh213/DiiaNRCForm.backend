using System.Text.Json.Serialization;

namespace DiiaNRCForm.Abstractions.Models;

public class FormSubmission
{
    [JsonPropertyName("signatureId")]
    public Guid? SignatureId { get; set; }
    
    [JsonPropertyName("_id")]
    public int? SubmissionId { get; set; }
}