namespace OrderTracking.Core.DTOs.Order;
public record CreateOrderDto(
    int CustomerId, 
    List<OrderItemDto> OrderItems
    );