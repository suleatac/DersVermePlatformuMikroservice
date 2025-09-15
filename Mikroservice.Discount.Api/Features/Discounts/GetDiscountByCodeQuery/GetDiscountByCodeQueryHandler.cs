
using Mikroservice.Discount.Api.Repositories;

namespace Mikroservice.Discount.Api.Features.Discounts.GetDiscountByCodeQuery
{
    public class GetDiscountByCodeQueryHandler(AppDbContext context) : IRequestHandler<GetDiscountByCodeQuery, ServiceResult<GetDiscountByCodeQueryResponse>>
    {
        public async Task<ServiceResult<GetDiscountByCodeQueryResponse>> Handle(GetDiscountByCodeQuery request, CancellationToken cancellationToken)
        {

            var hasDiscount = await context.Discounts
                .SingleOrDefaultAsync(x => x.Code == request.Code, cancellationToken); 
            
            if (hasDiscount == null)
            {
                return ServiceResult<GetDiscountByCodeQueryResponse>.Error("Discount not found.",HttpStatusCode.NotFound);
            }

            if (hasDiscount.ExpiredTime < DateTime.Now)
            {
                return ServiceResult<GetDiscountByCodeQueryResponse>
                    .Error("Discount has expired.", HttpStatusCode.BadRequest);

            }
        
            return ServiceResult<GetDiscountByCodeQueryResponse>.SuccessAsOK(
                new GetDiscountByCodeQueryResponse(hasDiscount.Code, hasDiscount.Rate));


        }
    }
}
