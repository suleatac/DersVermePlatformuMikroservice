using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mikroservice.web.Pages.Auth.SignUp;

namespace Mikroservice.web.Pages.Auth
{
    public class SignUpModel(SignUpService signUpService) : PageModel
    {
        [BindProperty] public SignUpViewModel SignUpViewModel { get; set; } = SignUpViewModel.GetExampleModel();
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await signUpService.CreateAccount(SignUpViewModel);

            if (result.IsSuccess)
            {
                // Redirect to a success page or login page
                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Fail.Title);
                if (string.IsNullOrEmpty(result.Fail.Detail) == false)
                {
                    ModelState.AddModelError(string.Empty, result.Fail.Detail);
                }
                return Page();
            }

        }
    }
}
