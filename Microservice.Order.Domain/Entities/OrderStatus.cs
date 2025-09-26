namespace Microservice.Order.Domain.Entities
{
    public enum OrderStatus
    {
        waitingForPayment = 1,
        Paid = 2,
        Cancelled = 3,
        Shipped = 4,
        Completed = 5

    }
}
