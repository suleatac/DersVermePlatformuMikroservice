using Asp.Versioning.Builder;
using Microservice.File.Api.Features.File.Delete;
using Microservice.File.Api.Features.File.Upload;

namespace Microservice.File.Api.Features.File
{
    public static class FileEndPointExt
    {
        public static void AddFileGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            var group = app.MapGroup("/api/v{version:apiVersion}/files").WithTags("File");
     
            group.UploadFileGroupItemEndpoint();
            group.DeleteFileGroupItemEndpoint();
            group.WithApiVersionSet(apiVersionSet);
            group.RequireAuthorization();
        }

    }
}
