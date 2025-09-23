using Microservice.Basket.Api.Features.Baskets.Dtos;
using Microservice.Shared;

namespace Microservice.Basket.Api.Features.Baskets.GetBasket
{
    public record GetBasketQuery:IRequestByServiceResult<BasketDto>;
}
