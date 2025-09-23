using MediatR;
using Microservice.Basket.Api.Features.Baskets.DeleteBasketItem;
using Microservice.Shared.Extentions;
using Microservice.Shared.Filters;

namespace Microservice.Basket.Api.Features.Baskets.GetBasket
{

    public static class GetBasketQueryEndPoint
    {

        public static RouteGroupBuilder GetBasketItemGroupEndpoint(this RouteGroupBuilder group)
        {

            group.MapGet("/user", async (IMediator mediator) => (await mediator.Send(new GetBasketQuery())).ToGenericResult())
            .WithName("GetBasket")
            .MapToApiVersion(1.0);
           

            return group;
        }

    }
}
