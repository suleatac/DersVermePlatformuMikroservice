using FluentValidation;

namespace Microservice.Payment.Api.Features.Payment.Create
{
    public class CreatePaymentCommandValidator: AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator() { 
            RuleFor(x => x.OrderCode)
                .NotEmpty().WithMessage("Order code is required.")
                .MaximumLength(20).WithMessage("Order code must not exceed 20 characters.");
            RuleFor(x => x.CardNumber).NotNull().WithMessage("Card number is required.")
                .CreditCard().WithMessage("Invalid card number.");
            RuleFor(x => x.CardHolderName).NotEmpty().WithMessage("Card holder name is required.");
            RuleFor(x => x.CardExpirationDate).NotEmpty().WithMessage("Card expiration date is required.")
                .Matches(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$").WithMessage("Expiration date must be in MM/YY format.");
            RuleFor(x => x.CardSecurityNumber).NotEmpty().WithMessage("Card security number is required.");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero.");



        }
    }
}
