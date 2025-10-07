using MediatR;
using Microservice.Shared.Extentions;
using Microservice.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Payment.Api.Features.Payment.Create
{
    public static class CreatePaymentCommandEndPoint
    {
        public static RouteGroupBuilder CreatePaymentGroupItemEndPoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async ([FromBody]CreatePaymentCommand createPaymentCommand, IMediator mediator) =>
           (await mediator.Send(createPaymentCommand)).ToGenericResult())
            .WithName("CreatePayment")
            .RequireAuthorization("password")
            .MapToApiVersion(1,0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
            return group;
        }
    }
}
