using MediatR;
using Microservice.Basket.Api.Features.Baskets.AddBasketItem;
using Microservice.Shared.Extentions;
using Microservice.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Basket.Api.Features.Baskets.DeleteBasketItem
{

    public static class DeleteBasketItemEndPoint
    {

        public static RouteGroupBuilder DeleteBasketItemGroupEndpoint(this RouteGroupBuilder group)
        {

            group.MapDelete("/item/{id:guid}", async (IMediator mediator, Guid id) =>(await mediator.Send(new DeleteBasketItemCommand(id))).ToGenericResult())
            .WithName("DeleteBasketItem")
            .MapToApiVersion(1.0)
            .AddEndpointFilter<ValidationFilter<DeleteBasketItemCommandValidator>>();

            return group;
        }

    }
}
