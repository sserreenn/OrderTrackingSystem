using FluentValidation;
using OrderTracking.Core.DTOs.Order;

namespace OrderTracking.Business.Validation;

public class OrderItemValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemValidator()
    {
        RuleFor(x => x.ProductName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Adet 0'dan büyük olmalıdır.");
        RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("Birim fiyat 0'dan büyük olmalıdır.");
    }
}