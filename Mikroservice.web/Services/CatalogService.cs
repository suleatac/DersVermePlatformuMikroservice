using Microsoft.AspNetCore.Mvc;
using Mikroservice.web.Pages.Instructor.ViewModel;
using Mikroservice.web.Services.Refit.CatalogService;
using System.Text.Json;

namespace Mikroservice.web.Services
{
    public class CatalogService(ICatalogRefitService catalogRefitService, ILogger<CatalogService> logger)
    {

        public async Task<ServiceResult<List<CategoryViewModel>>> GetCategoriesAsync()
        {
            var response = await catalogRefitService.GetCategoriesAsync();
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
                logger.LogError("Error retrieving categories: {StatusCode} - {Title} - {Detail}",
                    response.Error.StatusCode,
                    problemDetails?.Title,
                    problemDetails?.Detail);
                return ServiceResult<List<CategoryViewModel>>.Error("Fail to retrieve categories. Please try again later.");
               
            }

            var categories = response!.Content!.Select(c=>new CategoryViewModel(c.Id,c.Name)).ToList();

            return ServiceResult<List<CategoryViewModel>>.Success(categories);









        }



    }
}
