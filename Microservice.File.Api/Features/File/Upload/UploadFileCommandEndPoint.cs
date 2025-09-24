using MediatR;
using Microservice.Shared.Extentions;
using Microservice.Shared.Filters;

namespace Microservice.File.Api.Features.File.Upload
{
 
    public static class UploadFileCommandEndPoint
    {

        public static RouteGroupBuilder UploadFileGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (IMediator mediator, IFormFile file) =>
            {
                var result = await mediator.Send(new UploadFileCommand(file));
                return result.ToGenericResult();
            })
            .WithName("UploadFile")
            .DisableAntiforgery()
            .MapToApiVersion(1.0);
            return group;
        }

    }
}
