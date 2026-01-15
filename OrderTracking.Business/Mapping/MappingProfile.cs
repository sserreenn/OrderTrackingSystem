using AutoMapper;
using OrderTracking.Core.DTOs.Customer;
using OrderTracking.Core.DTOs.Order;
using OrderTracking.Core.Entities;

namespace OrderTracking.Business.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Customer Mappings
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<CreateCustomerDto, Customer>();

        // Order Mappings
        CreateMap<CreateOrderDto, Order>();
        CreateMap<OrderItemDto, OrderItem>().ReverseMap();

        CreateMap<Order, OrderDto>()
       .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
       .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

        CreateMap<Order, OrderListDto>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        // OrderItem Mappings
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();

        CreateMap<Order, OrderListDto>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}