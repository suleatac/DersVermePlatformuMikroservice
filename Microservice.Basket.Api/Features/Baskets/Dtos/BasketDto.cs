using System.Text.Json.Serialization;

namespace Microservice.Basket.Api.Features.Baskets.Dtos
{

    public record BasketDto
    {
       [JsonIgnore] public Guid UserId { get; init; }
        public List<BasketItemDto> BasketItems { get; set; } = new();
        public BasketDto(Guid userId, List<BasketItemDto> items) 
        {
            UserId = userId;
            BasketItems = items;
        }
        public BasketDto()
        {
         
        }


    }
}
