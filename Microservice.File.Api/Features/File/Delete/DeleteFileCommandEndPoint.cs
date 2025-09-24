using MediatR;
using Microservice.Shared.Extentions;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.File.Api.Features.File.Delete
{

    public static class DeleteFileCommandEndPoint
    {

        public static RouteGroupBuilder DeleteFileGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("", async (IMediator mediator, [FromBody]DeleteFileCommand deleteFileCommand) =>
            {
                var result = await mediator.Send(deleteFileCommand);
                return result.ToGenericResult();
            })
            .WithName("DeleteFile")
            .MapToApiVersion(1.0);
            return group;
        }

    }
}
