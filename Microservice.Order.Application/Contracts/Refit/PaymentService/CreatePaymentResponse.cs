namespace Microservice.Order.Application.Contracts.Refit.PaymentService
{
    public record CreatePaymentResponse( Guid? PaymentId, bool Status, string? ErrorMessage=null);
}
