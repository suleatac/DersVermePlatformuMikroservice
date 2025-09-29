using MediatR;
using Microservice.Order.Application.Features.Orders.GetOrders;
using Microservice.Order.Application.Features.Orders.CreateOrder;
using Microservice.Shared.Filters;
using Microservice.Shared.Extentions;

namespace Mikroservice.Order.Api.Endpoints.Orders
{
 
    public static class GetOrdersEndPoint
    {

        public static RouteGroupBuilder GetOrdersGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetOrdersQuery());
                return result.ToGenericResult();
            })
            .WithName("CreateOrder")
                 .MapToApiVersion(1.0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

            return group;
        }

    }
}
