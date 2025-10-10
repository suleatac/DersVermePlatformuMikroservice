using Microsoft.AspNetCore.Mvc.Rendering;
using Mikroservice.web.Pages.Auth.SignIn;
using System.ComponentModel.DataAnnotations;

namespace Mikroservice.web.Pages.Instructor.ViewModel
{
    public record CreateCourseViewModel
    {
      
        public static CreateCourseViewModel Empty=> new CreateCourseViewModel();


        [Display(Name = "Course Category")]
        public SelectList CategoryDropdownList { get; set; } = default!;
        [Display(Name = "Course Picture")]
        public IFormFile? PictureFormFile { get; init; }
        [Display(Name = "Course Name")]
        public string Name { get; init; } = default!;
        [Display(Name = "Course Description")]
        public string Description { get; init; }=default!;
        [Display(Name = "Course Price")]
        public decimal Price { get; init; }
        public Guid? CategoryId { get; init; }
        public void SetCategoryDropdownList(List<CategoryViewModel> categories)
        {
            CategoryDropdownList = new SelectList(categories, "Id", "Name");
        }

        //public static CreateCourseViewModel GetExampleModel()
        //{
        //    return new CreateCourseViewModel {
        //        Name = "honur@gmail.com",
        //        Description = "Password!"
        //    };
        //}




    }
}
