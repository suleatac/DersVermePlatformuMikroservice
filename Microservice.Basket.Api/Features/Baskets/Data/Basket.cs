using Microservice.Basket.Api.Features.Baskets.Dtos;
using System.Text.Json.Serialization;

namespace Microservice.Basket.Api.Features.Baskets.Data
{
    public class Basket
    {
        public Guid UserId { get; set; }
        public List<BasketItem> Items { get; set; } = new();
        public float? DiscountRate { get; set; }
        public string? CouponCode { get; set; }

        public bool IsApplyDiscount => DiscountRate is > 0 && !string.IsNullOrEmpty(CouponCode);
        public decimal TotalPrice => Items.Sum(x => x.Price);
        public decimal? TotalPriceWithAppliedDiscount => IsApplyDiscount ? Items.Sum(c => c.PriceByApplyDiscountRate) : TotalPrice;
        public void ApplyNewDiscount(string coupon, float discountRate)
        {
            CouponCode = coupon;
            DiscountRate = discountRate;
            foreach (var basketItem in Items)
            {
               basketItem.PriceByApplyDiscountRate = basketItem.Price - (basketItem.Price * (decimal)(DiscountRate / 100));
            }
        }
        public void ApplyAvailableDiscount()
        {
         
            foreach (var basketItem in Items)
            {
                basketItem.PriceByApplyDiscountRate = basketItem.Price - (basketItem.Price * (decimal)(DiscountRate / 100));
            }
        }
        public void RemoveDiscount()
        {
            CouponCode = null;
            DiscountRate = null;
            foreach (var basketItem in Items)
            {
                basketItem.PriceByApplyDiscountRate = null;
            }
        }


        public Basket(Guid userId, List<BasketItem> items)
        {
            UserId=userId;
            Items = items;
        }


        public Basket() { }





    }
}
