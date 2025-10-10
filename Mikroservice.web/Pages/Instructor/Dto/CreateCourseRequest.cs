namespace Mikroservice.web.Pages.Instructor.Dto
{
    public record CreateCourseRequest(
        string Name,
        string Description,
        decimal Price,
        Guid CategoryId,
        IFormFile? Picture
        );
}
