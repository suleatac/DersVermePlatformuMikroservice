using Microservice.Catalog.Api.Features.Courses.Dtos;
using Microservice.Catalog.Api.Features.Courses.GetAll;

namespace Microservice.Catalog.Api.Features.Courses.GetAll
{


    public record GetAllCourseQuery : IRequestByServiceResult<List<CourseDto>>;

    public class GetAllCourseQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllCourseQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCourseQuery request, CancellationToken cancellationToken)
        {
            var courses = await context.Courses.ToListAsync(cancellationToken: cancellationToken);
            var categories = await context.Categories.ToListAsync(cancellationToken: cancellationToken);

            foreach (var course in courses)
            {
                course.Category = categories.FirstOrDefault(c => c.Id == course.CategoryId);
            }
            var coursesAsDto = mapper.Map<List<CourseDto>>(courses);
            return ServiceResult<List<CourseDto>>.SuccessAsOK(coursesAsDto);
        }
    }


    public static class GetAllCourseEndPoint
    {
        public static RouteGroupBuilder GetAllCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/",
                async (IMediator mediator) =>
                (await mediator.Send(new GetAllCourseQuery())).ToGenericResult())
                     .MapToApiVersion(1.0)
                  .WithName("GetAllCourses");
            return group;
        }
    }
}












