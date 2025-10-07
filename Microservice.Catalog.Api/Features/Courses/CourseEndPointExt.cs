using Asp.Versioning.Builder;
using Microservice.Catalog.Api.Features.Courses.Create;
using Microservice.Catalog.Api.Features.Courses.Delete;
using Microservice.Catalog.Api.Features.Courses.GetAll;
using Microservice.Catalog.Api.Features.Courses.GetAllByUserId;
using Microservice.Catalog.Api.Features.Courses.GetById;
using Microservice.Catalog.Api.Features.Courses.Update;

namespace Microservice.Catalog.Api.Features.Courses
{
   
    public static class CourseEndPointExt
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            var group = app.MapGroup("/api/v{version:apiVersion}/courses").WithTags("Courses");
            group.CreateCourseGroupItemEndpoint();
            group.GetAllCourseGroupItemEndpoint();
            group.GetByIdCourseGroupItemEndPoint();
            group.UpdateCourseGroupItemEndpoint();
            group.DeleteCourseGroupItemEndpoint();
            group.GetByUserIdCourseGroupItemEndPoint();
            group.WithApiVersionSet(apiVersionSet);
            group.RequireAuthorization("password");
        }


    }
}
