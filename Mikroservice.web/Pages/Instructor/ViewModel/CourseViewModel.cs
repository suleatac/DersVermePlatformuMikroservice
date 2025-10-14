using Microservice.web.Pages.Instructor.Dto;
using Mikroservice.web.Pages.Instructor.Dto;

namespace Mikroservice.web.Pages.Instructor.ViewModel
{
    public record CourseViewModel(
        Guid Id,
        string Name,
        string Description,
        string ImageUrl,
        decimal Price,
        string CategoryName,
        int Duration,
        float Rating
        )
    {
        public string TruncateDescription(int maxLength)
        {
            if (Description.Length <= maxLength) { 
            return Description;
            }
            return Description.Substring(0, maxLength) + "...";
        }




    }
            
}
