using Microsoft.AspNetCore.Razor.TagHelpers;
using Mikroservice.web.Options;

namespace Mikroservice.web.TagHelpers
{
    public class CourseThumbnailPictureTagHelper(MicroserviceOption microserviceOption) : TagHelper
    {
        public string? Src { get; set; }
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            var blankCourseThumbnailImagePath = "/images/course-thumbnail.jpg";
           
            if (string.IsNullOrEmpty(Src))
            {
                output.Attributes.SetAttribute("src", blankCourseThumbnailImagePath);
            }
            else if (Src.StartsWith("http"))
            {
                output.Attributes.SetAttribute("src", Src);
            }
            else
            {
                var imageUrl = $"{microserviceOption.File.BaseUrl.TrimEnd('/')}/{Src.TrimStart('/')}";
                output.Attributes.SetAttribute("src", imageUrl);
            }

            return base.ProcessAsync(context, output);
        }
    }
}
