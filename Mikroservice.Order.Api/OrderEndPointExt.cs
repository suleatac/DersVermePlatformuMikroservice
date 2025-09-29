using Asp.Versioning.Builder;
using Mikroservice.Order.Api.Endpoints.Orders;

namespace Mikroservice.Order.Api
{

    public static class OrderEndPointExt
    {
        public static void AddOrderGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            var group = app.MapGroup("/api/v{version:apiVersion}/orders").WithTags("Orders");
            group.CreateOrderGroupItemEndpoint();
            group.GetOrdersGroupItemEndpoint();
            group.WithApiVersionSet(apiVersionSet);
        }


    }
}
