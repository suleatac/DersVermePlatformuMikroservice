using MediatR;
using Microservice.Shared;
using Microsoft.Extensions.FileProviders;

namespace Microservice.File.Api.Features.File.Upload
{
    public class UploadFileCommandHandler(IFileProvider fileProvider): IRequestHandler<UploadFileCommand, ServiceResult<UploadFileCommandResponse>>
    {
        public async Task<ServiceResult<UploadFileCommandResponse>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            if (request.File == null || request.File.Length <= 0)
            {
                return ServiceResult<UploadFileCommandResponse>.Error("Invalid file.", "No file uploaded.",System.Net.HttpStatusCode.BadRequest);
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }



            //burayı incele bunu copilot yazdı
            var uniqueFileName = $"{Guid.NewGuid()}_{request.File.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            //bunu videoda yazdık

            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";//.jpg .png .pdf  gibi...
            var uploadPath = Path.Combine(fileProvider.GetFileInfo("files").PhysicalPath!,newFileName);



            await using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream, cancellationToken);
            }
            var response = new UploadFileCommandResponse(newFileName, $"/files/{newFileName}", request.File.FileName);
            return ServiceResult<UploadFileCommandResponse>.SuccessAsCreated(response,response.FilePath);
        }
    }
}
