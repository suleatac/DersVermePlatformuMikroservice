using MediatR;
using Microservice.Shared;
using Microsoft.Extensions.FileProviders;

namespace Microservice.File.Api.Features.File.Delete
{
  
    public class DeleteFileCommandHandler(IFileProvider fileProvider) : IRequestHandler<DeleteFileCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            //var filePath = Path.Combine(fileProvider.GetFileInfo("files").PhysicalPath!, request.FileName);






            var fileInfo = fileProvider.GetFileInfo(Path.Combine("files", request.FileName));
            if (!fileInfo.Exists)
            {
                return ServiceResult.ErrorAsNotFound();
            }

            System.IO.File.Delete(fileInfo.PhysicalPath!);

            return ServiceResult.SuccessAsNoContent();
        }


    }
}
