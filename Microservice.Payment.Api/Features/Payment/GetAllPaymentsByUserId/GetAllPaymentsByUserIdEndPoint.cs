using MediatR;
using Microservice.Payment.Api.Features.Payment.Create;
using Microservice.Shared.Extentions;

namespace Microservice.Payment.Api.Features.Payment.GetAllPaymentsByUserId
{
    public static class GetAllPaymentsByUserIdEndPoint
    {
        public static RouteGroupBuilder GetAllPaymentsByUserIdGroupItemEndPoint(this RouteGroupBuilder group)
        {
            group.MapGet("", async (IMediator mediator) =>
           (await mediator.Send(new GetAllPaymentsByUserIdQuery())).ToGenericResult())
            .WithName("GetAllPaymentsByUserId")
           
            .MapToApiVersion(1, 0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
            return group;
        }
    }
}
