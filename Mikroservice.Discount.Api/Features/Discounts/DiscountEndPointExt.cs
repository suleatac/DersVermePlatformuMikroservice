using Asp.Versioning.Builder;
using Mikroservice.Discount.Api.Features.Discounts.CreateDiscount;
using Mikroservice.Discount.Api.Features.Discounts.GetDiscountByCode;

namespace Mikroservice.Discount.Api.Features.Discounts
{
    public static class DiscountEndPointExt
    {
        public static void AddDiscountGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            var group = app.MapGroup("/api/v{version:apiVersion}").WithTags("Courses");
            group.CreateDiscountGroupItemEndpoint();
            group.GetDiscountByCodeGroupItemEndpoint();
            group.WithApiVersionSet(apiVersionSet);
            group.RequireAuthorization();
        }


    }
}
