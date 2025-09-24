using AutoMapper;
using MediatR;
using Microservice.Basket.Api.Const;
using Microservice.Basket.Api.Features.Baskets.Dtos;
using Microservice.Shared;
using Microservice.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Microservice.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandHandler(BasketService basketService) : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
        {


     
            var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);
            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult<BasketDto>.Error("Basket not found", System.Net.HttpStatusCode.NotFound);
            }
            var currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

            if (!currentBasket.Items.Any())
            {
                return ServiceResult<BasketDto>.Error("Basket is empty, you can not apply discount", System.Net.HttpStatusCode.BadRequest);
            }



            currentBasket.ApplyNewDiscount(request.Coupon, request.DiscountRate);

            basketAsJson=JsonSerializer.Serialize(currentBasket);
            await basketService.CreateCacheAsync(currentBasket, cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
