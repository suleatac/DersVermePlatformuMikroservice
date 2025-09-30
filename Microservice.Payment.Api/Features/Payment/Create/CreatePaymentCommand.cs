using Microservice.Shared;

namespace Microservice.Payment.Api.Features.Payment.Create
{
    public record CreatePaymentCommand(
        string OrderCode, 
        string CardNumber, 
        string CardHolderName, 
        string CardExpirationDate, 
        string CardSecurityNumber, 
        decimal Amount):IRequestByServiceResult<Guid>;
}
