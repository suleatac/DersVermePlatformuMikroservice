using FluentValidation;
using FluentValidation.Validators;
using Microservice.Order.Application.Features.Orders.CreateOrder;

namespace Microservice.Catalog.Api.Features.Courses.CreateOrder
{


    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(c => c.DiscountRate).GreaterThan(0).When(x => x.DiscountRate.HasValue).WithMessage("{PropertyName} must be greater than zero");

            RuleFor(x => x.Address).NotNull().WithMessage("Address is required").SetValidator(new AddressDtoValidator());
            RuleForEach(x => x.Items).NotNull().WithMessage("Order items cannot be null");
            RuleForEach(x => x.Items).SetValidator(new OrderItemDtoValidator());
            RuleFor(x => x.Payment).SetValidator(new PaymentDtoValidator());

        }
    }

    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(c => c.ProductId).NotEmpty().WithMessage("{PropertyName} cannot be empty");
            RuleFor(c => c.UnitPrice).GreaterThan(0).WithMessage("{PropertyName} must be greater than zero");
        RuleFor(c=>c.ProductName).NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(200).WithMessage("{PropertyName} cannot exceed 200 characters");
        }
    }

    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator() { 
        
            RuleFor(c => c.Province).NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} cannot exceed 50 characters");
            RuleFor(c => c.District).NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} cannot exceed 50 characters");
            RuleFor(c => c.Street).NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(200).WithMessage("{PropertyName} cannot exceed 200 characters");
            RuleFor(c => c.ZipCode).NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(10).WithMessage("{PropertyName} cannot exceed 10 characters");
            RuleFor(c => c.Line).NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(500).WithMessage("{PropertyName} cannot exceed 500 characters");

        }
    }

    public class PaymentDtoValidator : AbstractValidator<PaymentDto>
    {
        public PaymentDtoValidator()
        {
            RuleFor(c => c.CardName).NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(200).WithMessage("{PropertyName} cannot exceed 200 characters");
            RuleFor(c => c.CardNumber).NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(16).WithMessage("{PropertyName} cannot exceed 16 characters");
            RuleFor(c => c.Expiration).NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(5).WithMessage("{PropertyName} cannot exceed 5 characters");
            RuleFor(c => c.CVV).NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(3).WithMessage("{PropertyName} cannot exceed 3 characters");
            RuleFor(c => c.Amount).GreaterThan(0).WithMessage("{PropertyName} must be greater than zero");
        }
    }
}
