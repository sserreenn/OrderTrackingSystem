using OrderTracking.Core.DTOs.Customer;

namespace OrderTracking.Business.Services.Abstarct;

public interface ICustomerService
{
    Task<List<CustomerDto>> GetAllCustomersAsync();
    Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto);
    Task<CustomerDto> GetCustomerByIdAsync(int id);
}
