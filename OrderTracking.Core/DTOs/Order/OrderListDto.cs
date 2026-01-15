namespace OrderTracking.Core.DTOs.Order;
public record OrderListDto(
    int Id, 
    int CustomerId, 
    string CustomerName, 
    DateTime OrderDate, 
    string Status, 
    decimal TotalAmount
    );