using DiiaNRCForm.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiiaNRCForm.Infrastructure.EntityTypeConfigs;

public abstract class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{ 
    public abstract void Configure(EntityTypeBuilder<T> builder);

    protected static void AddEntityProperties(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);
    }
}
