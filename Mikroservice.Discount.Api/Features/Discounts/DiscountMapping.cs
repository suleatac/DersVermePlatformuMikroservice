using Mikroservice.Discount.Api.Features.Discounts.CreateDiscount;

namespace Mikroservice.Discount.Api.Features.Discounts
{
    public class DiscountMapping : Profile
    {
        public DiscountMapping()
        {
            CreateMap<CreateDiscountCommand, Discount>();
        }
    }
}
