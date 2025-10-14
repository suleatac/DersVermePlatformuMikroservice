using Microservice.web.Services;
using Microsoft.AspNetCore.Mvc;
using Mikroservice.web.Pages.Instructor.ViewModel;
using Mikroservice.web.Services.Refit.CatalogService;
using Refit;
using System.Text.Json;

namespace Mikroservice.web.Services
{
    public class CatalogService(ICatalogRefitService catalogRefitService, UserService userService, ILogger<CatalogService> logger)
    {

        public async Task<ServiceResult<List<CategoryViewModel>>> GetCategoriesAsync()
        {
            var response = await catalogRefitService.GetCategoriesAsync();
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(response.Error.Content!);
                logger.LogError("Error retrieving categories: {StatusCode} - {Title} - {Detail}",
                    response.Error.StatusCode,
                    problemDetails?.Title,
                    problemDetails?.Detail);
                return ServiceResult<List<CategoryViewModel>>.Error("Fail to retrieve categories. Please try again later.");
               
            }

            var categories = response!.Content!.Select(c=>new CategoryViewModel(c.Id,c.Name)).ToList();

            return ServiceResult<List<CategoryViewModel>>.Success(categories);

        }

        public async Task<ServiceResult> CreateCourseAsync(CreateCourseViewModel createCourseViewModel)
        {
            StreamPart? pictureStreamPart = null;
            if (createCourseViewModel.PictureFormFile != null && createCourseViewModel.PictureFormFile.Length > 0)
            {
                var stream = createCourseViewModel.PictureFormFile.OpenReadStream();
                pictureStreamPart = new StreamPart(stream, createCourseViewModel.PictureFormFile.FileName, createCourseViewModel.PictureFormFile.ContentType);
            }

            var response = await catalogRefitService.CreateCourseAsync(
                createCourseViewModel.Name,
                createCourseViewModel.Description,
                createCourseViewModel.Price,
                createCourseViewModel.CategoryId.ToString()!,
                pictureStreamPart
                );

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(response.Error.Content!);
                logger.LogError("Error creating course: {StatusCode} - {Title} - {Detail}",
                    response.Error.StatusCode,
                    problemDetails?.Title,
                    problemDetails?.Detail);
                return ServiceResult.Error("Fail to create course. Please try again later.");
            }
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<CourseViewModel>>> GetCoursesByUserIdAsync()
        {
            var response = await catalogRefitService.GetCoursesById(userService.UserId);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(response.Error.Content!);
                logger.LogError("Error retrieving courses: {StatusCode} - {Title} - {Detail}",
                    response.Error.StatusCode,
                    problemDetails?.Title,
                    problemDetails?.Detail);
                return ServiceResult<List<CourseViewModel>>.Error("Fail to retrieve courses. Please try again later.");
            }
            var courses = response!.Content!.Select(c => new CourseViewModel(
                c.Id,
                c.Name,
                c.Description,
                c.ImageUrl,
                c.Price,
                c.Category.Name,
                c.Feature.Duration,
                c.Feature.Rating
                )).ToList();
            return ServiceResult<List<CourseViewModel>>.Success(courses);
        }

        //public async Task<ServiceResult> UpdateCourseAsync(UpdateCourseViewModel updateCourseViewModel)
        //{
        //    var request = new UpdateCourseRequest
        //    {
        //        Id = updateCourseViewModel.Id,
        //        Name = updateCourseViewModel.Name,
        //        Description = updateCourseViewModel.Description,
        //        Price = updateCourseViewModel.Price,
        //        CategoryId = updateCourseViewModel.CategoryId,
        //        UserId = userService.UserId,
        //        Feature = new Pages.Instructor.Dto.Feature
        //        {
        //            Duration = updateCourseViewModel.Duration,
        //            Rating = updateCourseViewModel.Rating
        //        }
        //    };
        //    var response = await catalogRefitService.UpdateCourseAsync(request);
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(response.Error.Content!);
        //        logger.LogError("Error updating course: {StatusCode} - {Title} - {Detail}",
        //            response.Error.StatusCode,
        //            problemDetails?.Title,
        //            problemDetails?.Detail);
        //        return ServiceResult.Error("Fail to update course. Please try again later.");
        //    }
        //    return ServiceResult.Success();
        //}
        public async Task<ServiceResult> DeleteCourseAsync(Guid courseId)
        {
            var response = await catalogRefitService.DeleteCourseAsync(courseId);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(response.Error.Content!);
                logger.LogError("Error deleting course: {StatusCode} - {Title} - {Detail}",
                    response.Error.StatusCode,
                    problemDetails?.Title,
                    problemDetails?.Detail);
                return ServiceResult.Error("Fail to delete course. Please try again later.");
            }
            return ServiceResult.Success();
        }


    }
}
