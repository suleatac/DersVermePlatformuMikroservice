using Microservice.web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mikroservice.web.Pages.Instructor.ViewModel;
using Mikroservice.web.Services;

namespace Mikroservice.web.Pages.Instructor
{
    public class CoursesModel(CatalogService catalogService) : PageModel
    {
        [BindProperty] public List<CourseViewModel> CourseViewModels { get; set; } = null!;
        public async Task<IActionResult> OnGetAsync()
        {

            var result = await catalogService.GetCoursesByUserIdAsync();
            if (result.IsFail)
            {
                // TODO: Loglama yapýlabilir. REDÝRECT TO ERROR PAGE edilebilir.
            }
            CourseViewModels = result.Data!;
            return Page();
        }
        public async Task<IActionResult> OnGetDeleteCourseAsync(Guid id)
        {
            var result = await catalogService.DeleteCourseAsync(id);
            if (result.IsFail)
            {
                // TODO: Loglama yapýlabilir. REDÝRECT TO ERROR PAGE edilebilir.
            }
            return RedirectToPage("Courses");
        }
    }
}
