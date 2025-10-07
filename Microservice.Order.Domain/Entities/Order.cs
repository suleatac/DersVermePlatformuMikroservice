namespace Microservice.Order.Domain.Entities
{
    public class Order:BaseEntity<Guid>
    {

        public string Code { get; set; } = null!;
        public DateTime Created { get; set; }
        public Guid BuyerId { get; set; }
        public OrderStatus Status { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public Guid PaymentId { get; set; }
        public float? DiscountRate { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


        public static string GenerateOrderCode()
        {
      
            return Guid.CreateVersion7().ToString();
        }

        public static Order CreateUnPaidOrder(Guid buyerId, int addressId, float? discountRate)
        {
   
            var order = new Order
            {
                Id=Guid.CreateVersion7(),
                Code = GenerateOrderCode(),
                Created = DateTime.UtcNow,
                BuyerId = buyerId,
                AddressId = addressId,
                DiscountRate = discountRate,
                Status = OrderStatus.waitingForPayment,
                TotalPrice = 0
            };
            return order;
        }

        public void AddOrderItem(Guid productId, string productName, decimal unitPrice)
        {


            if (DiscountRate.HasValue)
            {
                unitPrice -= unitPrice * (decimal)DiscountRate.Value / 100;
            }







            var orderItem= new OrderItem();
            orderItem.SetItem( productId, productName, unitPrice);

            var existingItem = OrderItems.FirstOrDefault(i => i.IsSameItem(orderItem));
            if (existingItem != null)
            {
                existingItem.UpdatePrice(unitPrice);
            }
            else
            {
                OrderItems.Add(orderItem);
            }
            RecalculateTotalPrice();
        }


        public void ApplyDiscount(float discountRate)
        {
            if (discountRate < 0 || discountRate > 100)
            {
                throw new ArgumentNullException("Discount percentage must be between 0 and 100");
            }
            this.DiscountRate = discountRate;
            RecalculateTotalPrice();
        }




        public void MarkAsPaid(Guid paymentId)
        {
            if (Status != OrderStatus.waitingForPayment)
            {
                throw new InvalidOperationException("Only orders waiting for payment can be marked as paid.");
            }
            this.Status = OrderStatus.Paid;
            this.PaymentId = paymentId;
        }



        private void RecalculateTotalPrice()
        {
            TotalPrice = OrderItems.Sum(i => i.UnitPrice);
          



        }


    }
}
