using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class DogConfiguration : IEntityTypeConfiguration<Dog>
{
    public void Configure(EntityTypeBuilder<Dog> builder)
    {
        builder.HasKey(x => x.Name);
        
        builder.Property(x => x.Name)
            .HasMaxLength(256)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(x => x.Color)
            .HasMaxLength(512)
            .HasColumnName("color")
            .IsRequired();
        
        builder.Property(x => x.TailLength)
            .HasColumnName("tail_length")
            .IsRequired();
        
        builder.Property(x => x.Weight)
            .HasColumnName("weight")
            .IsRequired();
    }
}