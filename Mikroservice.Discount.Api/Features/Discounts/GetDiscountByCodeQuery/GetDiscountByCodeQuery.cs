namespace Mikroservice.Discount.Api.Features.Discounts.GetDiscountByCodeQuery
{
    public record GetDiscountByCodeQuery(string Code):IRequestByServiceResult<GetDiscountByCodeQueryResponse>;
    
}
