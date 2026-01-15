namespace OrderTracking.Core.DTOs.Customer;

public class CreateCustomerDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}