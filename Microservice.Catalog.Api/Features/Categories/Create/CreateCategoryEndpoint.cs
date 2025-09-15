using Microservice.Shared.Filters;

namespace Microservice.Catalog.Api.Features.Categories.Create
{
    public static class CreateCategoryEndpoint
    {

        public static RouteGroupBuilder CreateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (IMediator mediator, CreateCategoryCommand command) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            })
            .WithName("CreateCategory")
            .MapToApiVersion(1.0)
            .AddEndpointFilter<ValidationFilter<CreateCategoryCommand>>();

            return group;
        }

    }
}
