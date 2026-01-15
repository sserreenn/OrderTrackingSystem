using FluentValidation;
using OrderTracking.Core.DTOs.Order;

namespace OrderTracking.Business.Validation;
public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.CustomerId).GreaterThan(0).WithMessage("Geçerli bir müşteri seçilmelidir.");

        RuleFor(x => x.OrderItems)
            .NotEmpty().WithMessage("Sipariş en az bir ürün içermelidir.");

        RuleForEach(x => x.OrderItems).SetValidator(new OrderItemValidator());
    }
}
