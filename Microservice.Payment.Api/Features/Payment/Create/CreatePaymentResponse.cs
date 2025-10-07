namespace Microservice.Payment.Api.Features.Payment.Create
{
    public record CreatePaymentResponse( Guid? PaymentId, bool Status, string? ErrorMessage=null);
}
