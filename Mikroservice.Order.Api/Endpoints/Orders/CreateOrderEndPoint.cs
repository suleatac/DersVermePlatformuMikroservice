using MediatR;
using Microservice.Catalog.Api.Features.Courses.Create;
using Microservice.Order.Application.Features.Orders.Create;
using Microservice.Shared.Extentions;
using Microservice.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Mikroservice.Order.Api.Endpoints.Orders
{

    public static class CreateOrderEndPoint
    {

        public static RouteGroupBuilder CreateOrderGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async ([FromServices] IMediator mediator, [FromBody]CreateOrderCommand command) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            })
            .WithName("CreateOrder")
                 .MapToApiVersion(1.0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .AddEndpointFilter<ValidationFilter<CreateOrderCommandValidator>>();

            return group;
        }

    }
}
