using MediatR;
using Microservice.Payment.Api.Features.Payment.Create;
using Microservice.Shared.Extentions;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Payment.Api.Features.Payment.GetStatus
{
    public static class GetPaymentStatusQueryEndPoint
    {
        public static RouteGroupBuilder GetPaymentStatusGroupItemEndPoint(this RouteGroupBuilder group)
        {
            group.MapGet("/status/{orderCode}", async ([FromServices] IMediator mediator, string orderCode) =>
           (await mediator.Send(new GetPaymentStatusRequest(orderCode))).ToGenericResult())
            .WithName("GetPaymentStatus")
            .RequireAuthorization("ClientCredential")
            .MapToApiVersion(1, 0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
            return group;
        }
    }
}
