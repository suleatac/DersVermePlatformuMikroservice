using MediatR;
using Microservice.Shared.Extentions;
using Microservice.Shared.Filters;

namespace Microservice.Basket.Api.Features.Baskets.AddBasketItem
{
 
    public static class AddBasketItemEndPoint
    {

        public static RouteGroupBuilder AddBasketItemGroupEndpoint(this RouteGroupBuilder group)
        {

            group.MapPost("/item", async (IMediator mediator, AddBasketItemCommand command) => (await mediator.Send(command)).ToGenericResult())
            .WithName("AddBasketItem")
            .MapToApiVersion(1.0)
            .AddEndpointFilter<ValidationFilter<AddBasketItemCommandValidator>>();

            return group;
        }

    }
}
