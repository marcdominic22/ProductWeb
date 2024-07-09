using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCRUD.Domain.Entities;

namespace ProductCRUD.Infrastructure;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.Property(t => t.SupplierName)
            .HasMaxLength(100);
    }
}