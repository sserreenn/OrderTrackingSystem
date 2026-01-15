namespace OrderTracking.Core.DTOs.Order;
public class CreateOrderDto
{
    public int CustomerId { get; set; }
    public List<OrderItemDto> OrderItems { get; set; } = new();
}