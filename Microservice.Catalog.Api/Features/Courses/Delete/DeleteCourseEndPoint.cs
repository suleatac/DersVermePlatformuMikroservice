using Microservice.Catalog.Api.Features.Courses.Dtos;

namespace Microservice.Catalog.Api.Features.Courses.Delete
{

    public record DeleteCourseCommand(Guid Id) : IRequestByServiceResult;
    public class DeleteCourseHandler(AppDbContext context) : IRequestHandler<DeleteCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await context.Courses.FindAsync(new object?[] { request.Id }, cancellationToken);
            if (course == null)
            {
                return ServiceResult.ErrorAsNotFound();
            }
            context.Courses.Remove(course);
            await context.SaveChangesAsync(cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }

    public static class DeleteCourseEndPoint
    {
        public static RouteGroupBuilder DeleteCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/{id:guid}", async (IMediator mediator, Guid id) =>
            {
                var result = await mediator.Send(new DeleteCourseCommand(id));
                return result.ToGenericResult<CourseDto>();
            })
                     .MapToApiVersion(1.0)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("DeleteCourse")
            .WithSummary("Delete Course")
            .WithDescription("Delete Course by Id");
            return group;
        }
    }
}
