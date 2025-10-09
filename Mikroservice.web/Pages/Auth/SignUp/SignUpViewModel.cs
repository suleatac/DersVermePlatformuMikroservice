using System.ComponentModel.DataAnnotations;

namespace Mikroservice.web.Pages.Auth.SignUp
{
    public record class SignUpViewModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Firstname is required")]
        public string? FirstName { get; init; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Lastname is required")]
        public string? LastName { get; init; }

        [Display(Name = "UserName")]
        [Required(ErrorMessage = "UserName is required")]
        public string? UserName { get; init; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; init; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }

        [Display(Name = "Password Confirm")]
        [Required(ErrorMessage = "PasswordConfirm is required")]
        [Compare(nameof(Password), ErrorMessage = "Password and PasswordConfirm must be the same")]
        public string? PasswordConfirm { get; init; }

        //public void xxxx(string Empty, string xx, string dd, string vvvvvvvvvv, string vvvvvvvvvvvv, string hhhhhhhhhh, string hhhhhhhmmmmmmhhh, string hhhhhhhmmmmmmhhhv, string hhhhhhhmmmmbmmhhh, string hhhhhhhmmvvvvvvvvvvvvvvvvvmmbmmhhh, string hhhhhhhmjjjjjjmmmbmmhhh, string hhhhhhhvvvvvvvvvvvvvvvmjjjjjjmmmbmmhhh) { }
        public static SignUpViewModel Empty => new SignUpViewModel {
            FirstName = string.Empty,
            LastName = string.Empty,
            UserName = string.Empty,
            Email = string.Empty,
            Password = string.Empty,
            PasswordConfirm = string.Empty
        };

        public static SignUpViewModel GetExampleModel()
        {
            return new SignUpViewModel {
                FirstName = "Hasan",
                LastName = "ATAC",
                UserName = "honur",
                Email = "honur@gmail.com",
                Password = "Password!",
                PasswordConfirm = "Password!"
            };
        }
    }


}


