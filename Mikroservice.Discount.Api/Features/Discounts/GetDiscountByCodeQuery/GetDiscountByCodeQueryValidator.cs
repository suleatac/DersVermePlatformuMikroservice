using Mikroservice.Discount.Api.Features.Discounts.CreateDiscount;

namespace Mikroservice.Discount.Api.Features.Discounts.GetDiscountByCodeQuery
{
   
    public class GetDiscountByCodeQueryValidator : AbstractValidator<GetDiscountByCodeQuery>
    {
        public GetDiscountByCodeQueryValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required");

          
        }
    }
}
