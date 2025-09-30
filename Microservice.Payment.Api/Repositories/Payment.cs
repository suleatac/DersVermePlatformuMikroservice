using MassTransit;

namespace Microservice.Payment.Api.Repositories
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string OrderCode { get; set; }
        public decimal Amount { get; set; }
        public DateTime Created { get; set; }
        public PaymentStatus Status { get; set; }

        public Payment(Guid userId, string orderCode, decimal amount)
        {
            Create(userId, orderCode, amount);
        }

        public void Create(Guid userId, string orderCode, decimal amount)
        {
            Id = NewId.NextSequentialGuid();
            UserId = userId;
            OrderCode = orderCode;
            Amount = amount;
            Created = DateTime.UtcNow;
            Status = PaymentStatus.Pending;
        }



    }

    public enum PaymentStatus
    {
        Success = 1,
        Failed = 2,
        Pending = 3
    }
}
