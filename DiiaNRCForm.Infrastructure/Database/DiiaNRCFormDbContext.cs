using DiiaNRCForm.Abstractions.Database;
using DiiaNRCForm.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiiaNRCForm.Infrastructure.Database;

public class DiiaNRCFormDbContext : DbContext, IDiiaNRCFormDbContext
{
    public DiiaNRCFormDbContext(DbContextOptions<DiiaNRCFormDbContext> options) : base(options)
    { }
    
    public DbSet<SignatureRequest> SignatureRequests { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}