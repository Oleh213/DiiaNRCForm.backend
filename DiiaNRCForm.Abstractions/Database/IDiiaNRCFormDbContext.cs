using DiiaNRCForm.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiiaNRCForm.Abstractions.Database;

public interface IDiiaNRCFormDbContext : IDisposable
{
    public DbSet<SignatureRequest> SignatureRequests { get; set; }
}