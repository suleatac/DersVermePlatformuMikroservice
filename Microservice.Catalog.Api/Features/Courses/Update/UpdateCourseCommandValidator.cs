using Microservice.Catalog.Api.Features.Categories.Create;

namespace Microservice.Catalog.Api.Features.Courses.Update
{
 

    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(100).WithMessage("{PropertyName} cannot exceed 100 characters");

            RuleFor(c => c.Description).NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(2000).WithMessage("{PropertyName} cannot exceed 2000 characters");

            RuleFor(c => c.Price).GreaterThan(0).WithMessage("{PropertyName} must be greater than zero");



            RuleFor(c => c.CategoryId).NotEmpty().WithMessage("{PropertyName} cannot be empty");
        }
    }
}
