namespace OrderTracking.Core.DTOs.Customer;

public record CreateCustomerDto(
    string Name, 
    string Email
    );