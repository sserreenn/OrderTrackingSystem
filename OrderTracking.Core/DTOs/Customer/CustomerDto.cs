namespace OrderTracking.Core.DTOs.Customer;
public class CustomerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
}
