using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderTracking.Core.Entities;

namespace OrderTracking.DataAccess.Configurations;
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(150);

        // Index ekleyerek sorgu performansını artıralım (Senior Touch)
        builder.HasIndex(x => x.Email).IsUnique();
    }
}