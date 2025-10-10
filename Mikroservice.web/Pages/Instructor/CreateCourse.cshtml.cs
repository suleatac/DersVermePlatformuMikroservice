using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mikroservice.web.Pages.Auth.SignIn;
using Mikroservice.web.Pages.Instructor.ViewModel;

namespace Mikroservice.web.Pages.Instructor
{
    public class CreateCourseModel : PageModel
    {
        [BindProperty] public CreateCourseViewModel CreateCourseViewModel { get; set; } = CreateCourseViewModel.Empty;
        public void OnGet()
        {
        }
    }
}
