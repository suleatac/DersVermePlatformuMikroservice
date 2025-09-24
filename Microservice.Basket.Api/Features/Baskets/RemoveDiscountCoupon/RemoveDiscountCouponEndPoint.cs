using MediatR;
using Microservice.Basket.Api.Const;
using Microservice.Basket.Api.Features.Baskets.DeleteBasketItem;
using Microservice.Shared;
using Microservice.Shared.Extentions;
using Microservice.Shared.Filters;
using Microservice.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace Microservice.Basket.Api.Features.Baskets.RemoveDiscountCoupon
{
    public record RemoveDiscountCouponCommand : IRequestByServiceResult;


    public class RemoveDiscountCouponHandler(BasketService basketService) : IRequestHandler<RemoveDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(RemoveDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

            //fast fail

            if (string.IsNullOrEmpty(basketAsJson))
            {

                return ServiceResult.Error("Basket not found", System.Net.HttpStatusCode.NotFound);
            }

            var currentBasket = System.Text.Json.JsonSerializer.Deserialize<Data.Basket>(basketAsJson);



            currentBasket!.RemoveDiscount();


            basketAsJson = System.Text.Json.JsonSerializer.Serialize(currentBasket);
            await basketService.CreateCacheAsync(currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();



        }


    }
    public static class RemoveDiscountCouponEndPoint
    {

        public static RouteGroupBuilder RemoveDiscountGroupItemEndpoint(this RouteGroupBuilder group)
        {

            group.MapDelete("/remove-discount-coupon", async (IMediator mediator) => (await mediator.Send(new RemoveDiscountCouponCommand())).ToGenericResult())
            .WithName("RemoveDiscountCoupon")
            .MapToApiVersion(1.0);

            return group;
        }

    }
}
