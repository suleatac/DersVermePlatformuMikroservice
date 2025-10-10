namespace Mikroservice.web.Pages.Instructor.Dto
{
    public record UpdateCourseRequest(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        Guid CategoryId,
        string? ImageUrl
        );
}
