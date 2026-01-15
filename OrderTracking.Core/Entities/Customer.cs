namespace OrderTracking.Core.Entities;
public class Customer : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;

    // Navigation Property: Bir müşterinin birden fazla siparişi olabilir.
    public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
}