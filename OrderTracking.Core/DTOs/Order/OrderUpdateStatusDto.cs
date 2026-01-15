namespace OrderTracking.Core.DTOs.Order;
public record OrderUpdateStatusDto(
    int Status
    ); 
// 1: Pending,
//2: Completed,
//3: Cancelled