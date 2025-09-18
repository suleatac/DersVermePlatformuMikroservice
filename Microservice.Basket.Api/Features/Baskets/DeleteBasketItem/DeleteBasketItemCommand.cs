using Microservice.Shared;

namespace Microservice.Basket.Api.Features.Baskets.DeleteBasketItem
{

    public record DeleteBasketItemCommand(
    Guid basketId) : IRequestByServiceResult;
}
