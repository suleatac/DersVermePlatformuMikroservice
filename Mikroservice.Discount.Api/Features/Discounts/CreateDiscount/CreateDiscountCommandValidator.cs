namespace Mikroservice.Discount.Api.Features.Discounts.CreateDiscount
{
 
    public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .Length(4, 25).WithMessage("{PropertyName} must be between 4 and 25 characters");
                
            RuleFor(x => x.Rate).NotEmpty().WithMessage("{PropertyName} cannot be empty");
            
            RuleFor(x => x.UserId).NotEqual(Guid.Empty).WithMessage("{PropertyName} must be a valid user ID");
            
            RuleFor(x => x.Expired).Must(date => date > DateTime.Now).WithMessage("{PropertyName} must be a future date");
        }
    }
}
