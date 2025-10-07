using Microservice.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Catalog.Api.Features.Courses.Create
{
    public static class CreateCourseCommandEndPoint
    {

        public static RouteGroupBuilder CreateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (IMediator mediator, [FromForm]CreateCourseCommand command) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            })
            .WithName("CreateCourse")
                 .MapToApiVersion(1.0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .AddEndpointFilter<ValidationFilter<CreateCourseCommand>>()
            .DisableAntiforgery();

            return group;
        }

    }

}
