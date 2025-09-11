using Microservice.Catalog.Api.Features.Categories.Create;
using Microservice.Catalog.Api.Features.Categories.GetAll;
using Microservice.Catalog.Api.Features.Categories.GetById;
using Microservice.Catalog.Api.Features.Courses.Create;

namespace Microservice.Catalog.Api.Features.Courses
{
   
    public static class CourseEndPointExt
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app)
        {
            var group = app.MapGroup("/api/courses").WithTags("Courses");
            group.CreateCourseGroupItemEndpoint();
            group.GetAllCourseGroupItemEndpoint();
            group.GetByIdCourseGroupItemEndPoint();
        }


    }
}
