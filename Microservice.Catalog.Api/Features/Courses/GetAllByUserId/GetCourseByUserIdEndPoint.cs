using Microservice.Catalog.Api.Features.Courses.Dtos;

namespace Microservice.Catalog.Api.Features.Courses.GetAllByUserId
{


    public record GetCourseByUserIdQuery(Guid Id) : IRequestByServiceResult<List<CourseDto>>;

    public class GetCourseByUserIdQueryHandler(AppDbContext context, IMapper mapper) :
       IRequestHandler<GetCourseByUserIdQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetCourseByUserIdQuery request,
            CancellationToken cancellationToken)
        {

            var courses = await context.Courses.Where(c => c.UserId == request.Id)
                .ToListAsync(cancellationToken: cancellationToken);
            var categories = await context.Categories.ToListAsync(cancellationToken: cancellationToken);

            foreach (var course in courses)
            {
                course.Category = categories.FirstOrDefault(c => c.Id == course.CategoryId);
            }
            var coursesAsDto = mapper.Map<List<CourseDto>>(courses);
            return ServiceResult<List<CourseDto>>.SuccessAsOK(coursesAsDto);
        }
    }

    public static class GetCourseByUserIdEndPoint
    {
        public static RouteGroupBuilder GetByUserIdCourseGroupItemEndPoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{userId:guid}",
                async (Guid userId, IMediator mediator, CancellationToken cancellationToken) =>
                    (await mediator.Send(new GetCourseByUserIdQuery(userId))).ToGenericResult())
                     .MapToApiVersion(1.0)
                .WithName("GetByUserIdCourse");
            return group;
        }
    }


    
}
