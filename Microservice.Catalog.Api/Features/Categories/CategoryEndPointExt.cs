using Microservice.Catalog.Api.Features.Categories.Create;
using Microservice.Catalog.Api.Features.Categories.GetAll;
using Microservice.Catalog.Api.Features.Categories.GetById;

namespace Microservice.Catalog.Api.Features.Categories
{
    public static class CategoryEndPointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app)
        {
            var group = app.MapGroup("/api/categories").WithTags("Categories");
            group.CreateCategoryGroupItemEndpoint();
            group.GetAllCategoryGroupItemEndpoint();
            group.GetByIdCategoryGroupItemEndPoint();
        }


    }
}
