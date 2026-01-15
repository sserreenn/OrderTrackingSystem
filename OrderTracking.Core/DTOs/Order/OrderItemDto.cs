namespace OrderTracking.Core.DTOs.Order;

public record OrderItemDto(
    string ProductName, 
    int Quantity, 
    decimal UnitPrice
    );