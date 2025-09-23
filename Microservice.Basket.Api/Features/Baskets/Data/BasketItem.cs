using AutoMapper.Configuration;

namespace Microservice.Basket.Api.Features.Baskets.Data
{
    public class BasketItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }=default!;//name nullable olamaz demek
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceByApplyDiscountRate { get; set; }

        public BasketItem(Guid id, string name, string? imageUrl, decimal price, decimal? priceByApplyDiscountRate)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            Price = price;
            PriceByApplyDiscountRate = priceByApplyDiscountRate;
        }
    }
}
