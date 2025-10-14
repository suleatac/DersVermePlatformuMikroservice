using Mikroservice.web.Pages.Instructor.Dto;

namespace Microservice.web.Pages.Instructor.Dto
{
  
    public record CourseDto(
        Guid Id, 
        string Name,
        decimal Price,
        string Description, 
        string ImageUrl,
        CategoryDto Category,
        FeatureDto Feature
        );
}
