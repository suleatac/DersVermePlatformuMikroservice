using Microservice.Shared.Filters;

namespace Mikroservice.Discount.Api.Features.Discounts.CreateDiscount
{
  
    public static class CreateDiscountEndpoint
    {

        public static RouteGroupBuilder CreateDiscountGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (IMediator mediator, CreateDiscountCommand command) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            })
            .WithName("CreateDiscount")
            .MapToApiVersion(1.0)
            .AddEndpointFilter<ValidationFilter<CreateDiscountCommand>>();

            return group;
        }

    }
}
