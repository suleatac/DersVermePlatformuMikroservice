using MediatR;
using Microservice.Shared.Extentions;
using Microsoft.AspNetCore.Mvc;

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
            });
         
            return group;
        }

    }
}
