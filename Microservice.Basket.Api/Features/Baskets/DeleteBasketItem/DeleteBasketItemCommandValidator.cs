using FluentValidation;
using Microservice.Basket.Api.Features.Baskets.AddBasketItem;

namespace Microservice.Basket.Api.Features.Baskets.DeleteBasketItem
{

    public class DeleteBasketItemCommandValidator : AbstractValidator<DeleteBasketItemCommand>
    {
        public DeleteBasketItemCommandValidator()
        {
            RuleFor(x => x.basketId).NotEmpty().WithMessage("{PropertyName} is required.");
           
        }
    }
}
