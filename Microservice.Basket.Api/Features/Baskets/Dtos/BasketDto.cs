using System.Text.Json.Serialization;

namespace Microservice.Basket.Api.Features.Baskets.Dtos
{

    public record BasketDto
    {

        public List<BasketItemDto> BasketItems { get; set; } = new();





        public float? DiscountRate { get; set; }
        public string? CouponCode { get; set; }
        [JsonIgnore]
        public bool IsApplyDiscount => DiscountRate is > 0 && !string.IsNullOrEmpty(CouponCode);
        public decimal TotalPrice => BasketItems.Sum(x => x.Price);
     
        public decimal? TotalPriceWithAppliedDiscount => IsApplyDiscount ? BasketItems.Sum(c => c.PriceByApplyDiscountRate) : TotalPrice;









        public BasketDto( List<BasketItemDto> items) 
        {
    
            BasketItems = items;
        }
        public BasketDto()
        {
         
        }


    }
}
