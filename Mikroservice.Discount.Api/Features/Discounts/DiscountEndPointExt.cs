using Asp.Versioning.Builder;

namespace Mikroservice.Discount.Api.Features.Discounts
{
    public static class DiscountEndPointExt
    {
        public static void AddDiscountGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            var group = app.MapGroup("/api/v{version:apiVersion}").WithTags("Courses");
            group.CreateCourseGroupItemEndpoint();
            group.GetAllCourseGroupItemEndpoint();
            group.GetByIdCourseGroupItemEndPoint();
            group.UpdateCourseGroupItemEndpoint();
            group.DeleteCourseGroupItemEndpoint();
            group.GetByUserIdCourseGroupItemEndPoint();
            group.WithApiVersionSet(apiVersionSet);
        }


    }
}
