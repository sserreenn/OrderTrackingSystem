using OrderTracking.Core.DTOs.Order;
using OrderTracking.Core.Enums;

namespace OrderTracking.Business.Services.Abstarct;
public interface IOrderService
{
    Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto);
    Task<List<OrderListDto>> GetOrdersWithPaginationAsync(int page, int pageSize);
    Task<OrderDto> GetOrderByIdAsync(int id);
    Task UpdateOrderStatusAsync(int id, OrderStatus newStatus);
}