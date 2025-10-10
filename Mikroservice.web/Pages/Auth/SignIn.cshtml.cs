using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mikroservice.web.Pages.Auth.SignIn;
using Mikroservice.web.Pages.Auth.SignUp;

namespace Mikroservice.web.Pages.Auth
{

    public class SignInModel(SignInService signInService) : PageModel
    {
        [BindProperty] public SignInViewModel SignInViewModel { get; set; } = SignInViewModel.GetExampleModel();

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await signInService.AuthenticateAsync(SignInViewModel);

            if (result.IsSuccess)
            {
                // Redirect to a success page or login page
                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Fail!.Title!);
                if (string.IsNullOrEmpty(result.Fail.Detail) == false)
                {
                    ModelState.AddModelError(string.Empty, result.Fail.Detail);
                }
                return Page();
            }
        }





        public async Task<IActionResult> OnPostSignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }







    }
}
