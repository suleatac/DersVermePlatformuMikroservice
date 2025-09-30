namespace Microservice.Payment.Api.Features.Payment.Create
{
    public record CreatePaymentResponse(bool Status, string? ErrorMessage=null);
}
