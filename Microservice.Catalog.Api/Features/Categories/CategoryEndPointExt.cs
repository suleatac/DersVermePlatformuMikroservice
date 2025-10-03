using Asp.Versioning.Builder;
using Microservice.Catalog.Api.Features.Categories.Create;
using Microservice.Catalog.Api.Features.Categories.GetAll;
using Microservice.Catalog.Api.Features.Categories.GetById;

namespace Microservice.Catalog.Api.Features.Categories
{
    public static class CategoryEndPointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            var group = app.MapGroup("/api/v{version:apiVersion}/categories").WithTags("Categories");
            group.CreateCategoryGroupItemEndpoint();
            group.GetAllCategoryGroupItemEndpoint();
            group.GetByIdCategoryGroupItemEndPoint();
            group.WithApiVersionSet(apiVersionSet);
            group.RequireAuthorization();
        }


    }
}
