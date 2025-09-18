namespace Microservice.Basket.Api.Features.Baskets.Dtos
{

    public record BasketItemDto(
        Guid Id, 
        string Name, 
        string ImageUrl, 
        decimal Price, 
        decimal? PriceByApplyDiscountRate);
}
