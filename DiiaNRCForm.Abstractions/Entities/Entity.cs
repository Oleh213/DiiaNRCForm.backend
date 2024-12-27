namespace DiiaNRCForm.Abstractions.Entities;

public class Entity
{
    public DateTimeOffset? Created { get; set; } = null!;

    public string? CreatedBy { get; set; } = null!;

    public Guid Id { get; set; }

    public DateTimeOffset? Modified { get; set; } = null!;

    public string? ModifiedBy { get; set; } = null!;
}
