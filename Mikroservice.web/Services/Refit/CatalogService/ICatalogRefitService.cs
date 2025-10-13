using Microsoft.AspNetCore.Mvc;
using Mikroservice.web.Pages.Instructor.Dto;
using Refit;

namespace Mikroservice.web.Services.Refit.CatalogService
{
    public interface ICatalogRefitService
    {
        [Get("/api/v1/categories")]
        Task<ApiResponse<List<CategoryDto>>> GetCategoriesAsync();

        [Post("/api/v1/courses")]
        Task<ApiResponse<object>> CreateCourseAsync(CreateCourseRequest request);

        [Put("/api/v1/courses")]
        Task<ApiResponse<object>> UpdateCourseAsync(UpdateCourseRequest request);
        [Delete("/api/v1/courses/{id}")]
        Task<ApiResponse<object>> DeleteCourseAsync(Guid id);
    }
}
