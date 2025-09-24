using Asp.Versioning.Builder;
using Microservice.Basket.Api.Features.Baskets.AddBasketItem;
using Microservice.Basket.Api.Features.Baskets.ApplyDiscountCoupon;
using Microservice.Basket.Api.Features.Baskets.DeleteBasketItem;
using Microservice.Basket.Api.Features.Baskets.GetBasket;
using Microservice.Basket.Api.Features.Baskets.RemoveDiscountCoupon;

namespace Microservice.Basket.Api.Features.Baskets
{
    public static class BasketEndPointExt
    {
        public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            var group = app.MapGroup("/api/v{version:apiVersion}/baskets").WithTags("Baskets");
            group.AddBasketItemGroupEndpoint();
            group.DeleteBasketItemGroupEndpoint();
            group.GetBasketItemGroupEndpoint();
            group.ApplyDiscountCouponGroupItemEndpoint();
            group.RemoveDiscountGroupItemEndpoint();
            group.WithApiVersionSet(apiVersionSet);
        }

    }
}
