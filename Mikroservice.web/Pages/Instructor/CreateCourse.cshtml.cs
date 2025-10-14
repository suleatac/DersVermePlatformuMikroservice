using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mikroservice.web.Pages.Auth.SignIn;
using Mikroservice.web.Pages.Instructor.ViewModel;
using Mikroservice.web.Services;

namespace Mikroservice.web.Pages.Instructor
{
    [Authorize(Roles = "E�itmen Rol�")]
    public class CreateCourseModel(CatalogService catalogService) : PageModel
    {
        [BindProperty] public CreateCourseViewModel CreateCourseViewModel { get; set; } = CreateCourseViewModel.Empty;
       
        public async Task<IActionResult> OnGetAsync()
        {
            var categoriesResult = await catalogService.GetCategoriesAsync();
            if (categoriesResult.IsFail)
            {
                // TODO: Loglama yap�labilir. RED�REC TO ERROR PAGE edilebilir.


            }
            CreateCourseViewModel.SetCategoryDropdownList(categoriesResult.Data!);
           return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await catalogService.CreateCourseAsync(CreateCourseViewModel);
            if (result.IsFail)
            {
                // TODO: Loglama yap�labilir. RED�REC TO ERROR PAGE edilebilir.


            }
           
            return RedirectToPage("Courses");
        }




    }
}
