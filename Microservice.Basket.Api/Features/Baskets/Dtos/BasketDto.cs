namespace Microservice.Basket.Api.Features.Baskets.Dtos
{
    
    public record BasketDto(Guid UserId, List<BasketItemDto> BasketItems);
}
