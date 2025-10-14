using Microservice.Bus.Events;
using Microservice.Shared.Services;

namespace Microservice.Catalog.Api.Features.Courses.Create
{

    public class CreateCourseCommandHandler(
        AppDbContext context,
        IMapper mapper,
        IPublishEndpoint publishEndpoint,
        IIdentityService identityService) : IRequestHandler<CreateCourseCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
        
            var hasCategory = await context.Categories.AnyAsync(c => c.Id == request.CategoryId, cancellationToken);
            if (!hasCategory)
            {
                return ServiceResult<Guid>.Error("Category was not found.", $"The Category with id({request.CategoryId}) was not found", HttpStatusCode.NotFound);
            }

            var hasCourse = await context.Courses.AnyAsync(c => c.Name == request.Name, cancellationToken);
            if (hasCourse)
            {
                return ServiceResult<Guid>.Error("Course already exists.", $"The Course with name({request.Name}) already exists", HttpStatusCode.BadRequest);
            }


            var newCourse = mapper.Map<Course>(request);
            newCourse.CreatedDate = DateTime.UtcNow;
            newCourse.Id=NewId.NextSequentialGuid();
            newCourse.UserId = identityService.UserId;
            newCourse.Feature = new Feature()
            {
                Duration = 10,
                EducatorFullName = "Eğitmen Şule ATAÇ",
                Rating = 0
            };
            context.Courses.Add(newCourse);
            await context.SaveChangesAsync(cancellationToken);

            if (request.Picture is not null)
            {
                using var memoryStream = new MemoryStream();
                await request.Picture.CopyToAsync(memoryStream, cancellationToken);
                var pictureAsArray = memoryStream.ToArray();
                var uploadPictureCommand = new UploadCoursePictureCommand(newCourse.Id, pictureAsArray, request.Picture.FileName);
                await publishEndpoint.Publish(uploadPictureCommand,cancellationToken);
            }




            return ServiceResult<Guid>.SuccessAsCreated(newCourse.Id,$"/api/courses/{newCourse.Id}");



        }
    }




}
