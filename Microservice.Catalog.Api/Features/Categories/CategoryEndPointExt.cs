using Microservice.Catalog.Api.Features.Categories.Create;

namespace Microservice.Catalog.Api.Features.Categories
{
    public static class CategoryEndPointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app)
        {
            var group = app.MapGroup("/api/categories");
            group.CreateCategoryGroupItemEndpoint();
        }


    }
}
