namespace OrderTracking.Core.DTOs.Order;
public class OrderUpdateStatusDto
{
    public int Status { get; set; }

    // 1: Pending,
    //2: Completed,
    //3: Cancelled
}