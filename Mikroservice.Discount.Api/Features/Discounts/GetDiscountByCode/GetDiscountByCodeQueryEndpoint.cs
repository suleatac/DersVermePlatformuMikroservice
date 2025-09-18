using Microservice.Shared.Filters;

namespace Mikroservice.Discount.Api.Features.Discounts.GetDiscountByCode
{
  
    public static class GetDiscountByCodeQueryEndpoint
    {

        public static RouteGroupBuilder GetDiscountByCodeGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{code:length(10)}", async (IMediator mediator, string code) =>
            {
                var result = await mediator.Send(new GetDiscountByCodeQuery(code));
                return result.ToGenericResult();
            })
            .WithName("GetDiscountByCode")
            .MapToApiVersion(1.0);

            return group;
        }

    }
}
