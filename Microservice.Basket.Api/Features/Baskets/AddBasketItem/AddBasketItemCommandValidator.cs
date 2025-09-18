using FluentValidation;

namespace Microservice.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandValidator: AbstractValidator<AddBasketItemCommand>
    {
        public AddBasketItemCommandValidator()
        {
           RuleFor(x => x.CourseId).NotEmpty().WithMessage("{PropertyName} is required.");
             RuleFor(x => x.CourseName).NotEmpty().MaximumLength(300).WithMessage("{PropertyName} is required.");
              RuleFor(x => x.CoursePrice).GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        }
    }
}
