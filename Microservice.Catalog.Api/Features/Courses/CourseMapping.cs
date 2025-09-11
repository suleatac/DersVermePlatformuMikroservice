using Microservice.Catalog.Api.Features.Categories;
using Microservice.Catalog.Api.Features.Courses.Create;

namespace Microservice.Catalog.Api.Features.Courses
{
    
    public class CourseMapping : Profile
    {
        public CourseMapping()
        {
            CreateMap<CreateCourseCommand, Course>();
        }
    }
}
