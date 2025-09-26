using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Order.Domain.Entities
{
    public class OrderItem : BaseEntity<int>
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public void SetItem(Guid productId, string productName, decimal unitPrice)
        {
            if (string.IsNullOrEmpty(ProductName))
            {

                throw new ArgumentNullException(nameof(productName), "Product Name can not be empty");

            }


            if (UnitPrice <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit Price can not be less than or equal to zero");
            }





            this.ProductId = productId;
            this.ProductName = productName;
            this.UnitPrice = unitPrice;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0)
            {
                throw new ArgumentNullException("Unit Price can not be less than or equal to zero");
            }
            this.UnitPrice = newPrice;
        }


        public void ApplyDiscount(double discountAmount)
        {
            if (discountAmount < 0 || discountAmount > 100)
            {
                throw new ArgumentNullException("Discount percentage must be between 0 and 100");
            }
            this.UnitPrice = this.UnitPrice - (this.UnitPrice * (decimal)(discountAmount / 100));
        }


        public bool IsSameItem(OrderItem otherItem)
        {
            return this.ProductId == otherItem.ProductId;
        }



    }
}
