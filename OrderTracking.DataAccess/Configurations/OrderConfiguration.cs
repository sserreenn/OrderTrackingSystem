using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderTracking.Core.Entities;

namespace OrderTracking.DataAccess.Configurations;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.Status).HasConversion<int>(); // Enum'ı int olarak sakla

        // Müşteri ile ilişki
        builder.HasOne(x => x.Customer)
               .WithMany(x => x.Orders)
               .HasForeignKey(x => x.CustomerId)
               .OnDelete(DeleteBehavior.Restrict); // Müşteri silinince siparişler kalmalı (Güvenlik)
    }
}