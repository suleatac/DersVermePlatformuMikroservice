using Microservice.File.Api.Features.File.Upload;
using Microservice.Shared;

namespace Microservice.File.Api.Features.File.Delete
{
 
    public record DeleteFileCommand(string FileName) : IRequestByServiceResult;
}
