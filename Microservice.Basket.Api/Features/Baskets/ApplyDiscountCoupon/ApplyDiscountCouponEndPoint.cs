using MediatR;
using Microservice.Basket.Api.Features.Baskets.AddBasketItem;
using Microservice.Shared.Extentions;
using Microservice.Shared.Filters;

namespace Microservice.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public static class ApplyDiscountCouponEndPoint
    {

        public static RouteGroupBuilder ApplyDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
        {

            group.MapPut("/apply-discount-rate", async (IMediator mediator, ApplyDiscountCouponCommand command) => (await mediator.Send(command)).ToGenericResult())
            .WithName("ApplyDiscountRate")
            .MapToApiVersion(1.0)
            .AddEndpointFilter<ValidationFilter<ApplyDiscountCouponCommandValidator>>();

            return group;
        }

    }
}
