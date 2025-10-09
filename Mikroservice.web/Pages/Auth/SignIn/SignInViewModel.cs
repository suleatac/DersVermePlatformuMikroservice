using System.ComponentModel.DataAnnotations;

namespace Mikroservice.web.Pages.Auth.SignIn
{

    public record class SignInViewModel
    {


        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; init; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }

      

        //public void xxxx(string Empty, string xx, string dd, string vvvvvvvvvv, string vvvvvvvvvvvv, string hhhhhhhhhh, string hhhhhhhmmmmmmhhh, string hhhhhhhmmmmmmhhhv, string hhhhhhhmmmmbmmhhh, string hhhhhhhmmvvvvvvvvvvvvvvvvvmmbmmhhh, string hhhhhhhmjjjjjjmmmbmmhhh, string hhhhhhhvvvvvvvvvvvvvvvmjjjjjjmmmbmmhhh) { }
        public static SignInViewModel Empty => new SignInViewModel {
            Email = string.Empty,
            Password = string.Empty
        };

        public static SignInViewModel GetExampleModel()
        {
            return new SignInViewModel {
                Email = "honur@gmail.com",
                Password = "Password!"
            };
        }
    }
}
