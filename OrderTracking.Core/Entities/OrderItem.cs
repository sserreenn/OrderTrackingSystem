namespace OrderTracking.Core.Entities;
public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    // Navigation Property
    public virtual Order Order { get; set; } = null!;
}