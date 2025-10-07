using MassTransit;
using Microservice.Bus.Events;
using Microsoft.Extensions.FileProviders;

namespace Microservice.File.Api.Consumers
{
    public class UploadCoursePictureCommandConsumer(IServiceProvider serviceProvider) : IConsumer<UploadCoursePictureCommand>
    {
        public async Task Consume(ConsumeContext<UploadCoursePictureCommand> context)
        {
            using var scope = serviceProvider.CreateScope();
            var fileProvider = scope.ServiceProvider.GetRequiredService<IFileProvider>();
            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(context.Message.FileName)}";//.jpg .png .pdf  gibi...
            var uploadPath = Path.Combine(fileProvider.GetFileInfo("files").PhysicalPath!, newFileName);
           
            
            
            
            
            
            
            
            
            
            
            await System.IO.File.WriteAllBytesAsync(uploadPath, context.Message.picture);

            var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
            await publishEndpoint.Publish(new CoursePictureUploadedEvent(context.Message.courseId, $"files/{newFileName}"));



        }
    }
}
