using Microservice.Catalog.Api.Features.Courses.Dtos;

namespace Microservice.Catalog.Api.Features.Courses.GetById
{
    public record GetCourseByIdQuery(Guid Id) : IRequestByServiceResult<CourseDto>;

    public class GetCourseByIdQueryHandler(AppDbContext context, IMapper mapper) :
       IRequestHandler<GetCourseByIdQuery, ServiceResult<CourseDto>>
    {
        public async Task<ServiceResult<CourseDto>> Handle(GetCourseByIdQuery request,
            CancellationToken cancellationToken)
        {

            var hasCourse = await context.Courses.FindAsync(request.Id, cancellationToken);


            if (hasCourse == null)
            {
                return ServiceResult<CourseDto>.Error("Course not found",
                    $"The Course with id({request.Id}) not found.", HttpStatusCode.NotFound);
            }

            var hasCategory = await context.Categories.FindAsync(hasCourse.CategoryId, cancellationToken);
            hasCourse.Category = hasCategory!;


            var courseAsDto = mapper.Map<CourseDto>(hasCourse);
            return ServiceResult<CourseDto>.SuccessAsOK(courseAsDto);
        }
    }

    public static class GetCourseByIdEndPoint
    {
        public static RouteGroupBuilder GetByIdCourseGroupItemEndPoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}",
                async (Guid id, IMediator mediator, CancellationToken cancellationToken) =>
                    (await mediator.Send(new GetCourseByIdQuery(id))).ToGenericResult())
                    .MapToApiVersion(1.0)
                    .WithName("GetByIdCourse");
            return group;
        }
    }
}
