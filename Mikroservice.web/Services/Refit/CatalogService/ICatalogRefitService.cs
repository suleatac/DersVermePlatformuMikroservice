using Microservice.web.Pages.Instructor.Dto;
using Microsoft.AspNetCore.Mvc;
using Mikroservice.web.Pages.Instructor.Dto;
using Refit;

namespace Mikroservice.web.Services.Refit.CatalogService
{
    public interface ICatalogRefitService
    {
        [Get("/api/v1/categories")]
        Task<ApiResponse<List<CategoryDto>>> GetCategoriesAsync();


        [Get("/api/v1/courses/user/{userId}")]
        Task<ApiResponse<List<CourseDto>>> GetCoursesById(Guid userId);



        [Multipart]
        [Post("/api/v1/courses")]
        Task<ApiResponse<object>> CreateCourseAsync(
            [AliasAs("Name")] string Name,
            [AliasAs("Description")] string Description,
            [AliasAs("Price")] decimal Price,
            [AliasAs("CategoryId")] string CategoryId,
            [AliasAs("Picture")] StreamPart? picture = null
            );

        [Put("/api/v1/courses")]
        Task<ApiResponse<object>> UpdateCourseAsync(UpdateCourseRequest request);
        [Delete("/api/v1/courses/{id}")]
        Task<ApiResponse<object>> DeleteCourseAsync(Guid id);
    }
}
