using Microservice.Catalog.Api.Features.Courses.Dtos;
using Microservice.Shared.Filters;

namespace Microservice.Catalog.Api.Features.Courses.Update
{
   
    public static class UpdateCourseCommandEndPoint
    {

        public static RouteGroupBuilder UpdateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/", async (IMediator mediator, UpdateCourseCommand command) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            })
            .MapToApiVersion(1.0)
            .AddEndpointFilter<ValidationFilter<UpdateCourseCommand>>()
            .WithName("UpdateCourse")
            .RequireAuthorization(policyNames: "Instructor");

            return group;
        }

    }






}
