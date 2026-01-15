namespace OrderTracking.Core.DTOs.Customer;

public record CustomerDto(
    int Id,
    string Name,
    string Email,
    DateTime CreatedDate
    );
