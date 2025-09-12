namespace Microservice.Catalog.Api.Features.Courses.Dtos
{
  
    public record CourseDto(
        Guid Id, 
        string Name, 
        string Description, 
        string ImageUrl, 
        CategoryDto Category,
        FeatureDto Feature
        );
}
