using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OrderTracking.Business.Services.Abstarct;
using OrderTracking.Core.DTOs.Customer;
using OrderTracking.Core.Entities;
using OrderTracking.Core.Interfaces;

namespace OrderTracking.Business.Services.Concreate;
public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private const string CustomerCacheKey = "CustomerList";

    public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _memoryCache = memoryCache;
    }

    public async Task<List<CustomerDto>> GetAllCustomersAsync()
    {
        // 1. Cache'de veri var mı kontrol et
        if (!_memoryCache.TryGetValue(CustomerCacheKey, out List<CustomerDto> customerList))
        {
            // 2. Yoksa DB'den çek
            var customers = await _unitOfWork.Repository<Customer>().GetAll().ToListAsync();
            customerList = _mapper.Map<List<CustomerDto>>(customers);

            // 3. Cache seçeneklerini ayarla (5 dakika sakla)
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                .SetSlidingExpiration(TimeSpan.FromMinutes(1)) // 1 dk boyunca hiç erişilmezse sil
                .SetPriority(CacheItemPriority.Normal);

            // 4. Cache'e ekle
            _memoryCache.Set(CustomerCacheKey, customerList, cacheOptions);
        }

        return customerList;
    }

    public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto)
    {
        var exists = await _unitOfWork.Repository<Customer>()
            .GetAll()
            .AnyAsync(x => x.Email == dto.Email);

        if (exists) throw new Exception("Bu e-posta adresi ile zaten bir müşteri kayıtlı.");

        var customer = _mapper.Map<Customer>(dto);
        customer.CreatedDate = DateTime.Now;

        await _unitOfWork.Repository<Customer>().AddAsync(customer);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<CustomerDto> GetCustomerByIdAsync(int id)
    {
        var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(id);
        if (customer == null) throw new Exception("Müşteri bulunamadı.");

        return _mapper.Map<CustomerDto>(customer);
    }
}