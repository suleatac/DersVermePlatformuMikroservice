﻿using FluentValidation;
using Microservice.Basket.Api.Features.Baskets.AddBasketItem;

namespace Microservice.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{


    public class ApplyDiscountCouponCommandValidator : AbstractValidator<ApplyDiscountCouponCommand>
    {
        public ApplyDiscountCouponCommandValidator()
        {
            RuleFor(x => x.Coupon).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.DiscountRate).GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        }
    }
}
