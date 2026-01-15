using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OrderTracking.Business.Services.Abstarct;
using OrderTracking.Core.DTOs.Order;
using OrderTracking.Core.Entities;
using OrderTracking.Core.Enums;
using OrderTracking.Core.Interfaces;

namespace OrderTracking.Business.Services.Concreate;
public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private const string OrderCacheKey = "OrderList";
    public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _memoryCache = memoryCache;
    }

    public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
    {
        // KURAL 1: Aynı müşteri aynı gün en fazla 5 sipariş verebilir.
        var today = DateTime.Today;
        var count = _unitOfWork.Repository<Order>()
            .GetAll()
            .Count(x => x.CustomerId == dto.CustomerId && x.OrderDate >= today);

        if (count >= 5)
            throw new Exception("Bir müşteri aynı gün içerisinde en fazla 5 sipariş oluşturabilir.");

        var order = _mapper.Map<Order>(dto);
        order.OrderDate = DateTime.Now;
        order.Status = OrderStatus.Pending;

        // KURAL 2: TotalAmount, OrderItems üzerinden hesaplanmalıdır.
        order.TotalAmount = dto.OrderItems.Sum(x => x.Quantity * x.UnitPrice);

        await _unitOfWork.Repository<Order>().AddAsync(order);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<OrderDto>(order);
    }

    public async Task<List<OrderListDto>> GetOrdersWithPaginationAsync(int page, int pageSize)
    {
        string dynamicCacheKey = $"{OrderCacheKey}_{page}_{pageSize}";

        if (!_memoryCache.TryGetValue(dynamicCacheKey, out List<OrderListDto> orderList))
        {
            // 1. Veritabanından çek (orders değişkenine değil, orderList'e atıyoruz)
            var query = await _unitOfWork.Repository<Order>()
                .GetAll()
                .Include(x => x.Customer)
                .OrderByDescending(x => x.OrderDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // 2. AutoMapper ile DTO'ya dönüştür ve sonucu doldur
            orderList = _mapper.Map<List<OrderListDto>>(query);

            // 3. Cache seçeneklerini ayarla
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                .SetSlidingExpiration(TimeSpan.FromMinutes(1));

            // 4. Cache'e ekle 
            _memoryCache.Set(dynamicCacheKey, orderList, cacheOptions);
        }

        return orderList;
    }

    public async Task<OrderDto> GetOrderByIdAsync(int id)
    {
        var order = await _unitOfWork.Repository<Order>()
            .GetAll()
            .Include(x => x.OrderItems) // Kalemleri de getir
            .FirstOrDefaultAsync(x => x.Id == id);

        if (order == null) throw new Exception("Sipariş bulunamadı.");

        return _mapper.Map<OrderDto>(order);
    }

    public async Task UpdateOrderStatusAsync(int id, OrderStatus newStatus)
    {
        var order = await _unitOfWork.Repository<Order>().GetByIdAsync(id);
        if (order == null) throw new Exception("Sipariş bulunamadı.");

        // KURAL 3: Cancelled durumundaki siparişler güncellenemez.
        if (order.Status == OrderStatus.Cancelled)
            throw new Exception("İptal edilmiş siparişler güncellenemez.");

        // KURAL 4: Status değişimi kuralları (Pending -> Completed/Cancelled vb.)
        if (order.Status == OrderStatus.Completed)
            throw new Exception("Tamamlanmış siparişler değiştirilemez.");

        order.Status = newStatus;
        _unitOfWork.Repository<Order>().Update(order);
        await _unitOfWork.CommitAsync();
    }
}