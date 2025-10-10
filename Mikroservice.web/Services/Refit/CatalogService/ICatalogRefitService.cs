using Microsoft.AspNetCore.Mvc;
using Mikroservice.web.Pages.Instructor.Dto;
using Refit;

namespace Mikroservice.web.Services.Refit.CatalogService
{
    public interface ICatalogRefitService
    {
        [HttpGet("/v1/catalog/categories")]
        Task<ApiResponse<ServiceResult>> GetCategoriesAsync();



        [Post("/v1/catalog/courses")]
        Task<ApiResponse<ServiceResult>> CreateCourseAsync(CreateCourseRequest request);

        [Put("/v1/catalog/courses")]
        Task<ApiResponse<ServiceResult>> UpdateCourseAsync(UpdateCourseRequest request);
        [Delete("/v1/catalog/courses/{id}")]
        Task<ApiResponse<ServiceResult>> DeleteCourseAsync(Guid id);
    }
}
