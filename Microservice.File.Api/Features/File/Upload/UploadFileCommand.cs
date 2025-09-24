using Microservice.Shared;

namespace Microservice.File.Api.Features.File.Upload
{
    public record UploadFileCommand(IFormFile File):IRequestByServiceResult<UploadFileCommandResponse>;
}
