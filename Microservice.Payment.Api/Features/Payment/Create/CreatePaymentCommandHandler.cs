using MediatR;
using Microservice.Payment.Api.Repositories;
using Microservice.Shared;
using Microservice.Shared.Services;

namespace Microservice.Payment.Api.Features.Payment.Create
{
    public class CreatePaymentCommandHandler(AppDbContext appDbContext, IIdentityService identityService,IHttpContextAccessor httpContextAccessor):IRequestHandler<CreatePaymentCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {

            var claims = httpContextAccessor.HttpContext?.User.Claims;  




            var (isSuccess,errorMessage) = await  ExternalPaymentProcessAsync(request.CardNumber, request.CardHolderName, request.CardExpirationDate, request.CardSecurityNumber, request.Amount);
            if (!isSuccess)
            {
                return ServiceResult<Guid>.Error("Payment failed",errorMessage!, System.Net.HttpStatusCode.BadRequest);
            }
        
            var payment = new Repositories.Payment(identityService.UserId, request.OrderCode, request.Amount);
            payment.Status = PaymentStatus.Success;
            appDbContext.Payments.Add(payment);
            await appDbContext.SaveChangesAsync(cancellationToken);
            return ServiceResult<Guid>.SuccessAsOK(payment.Id);
        }

        private async Task<(bool isSuccess,string? error)> ExternalPaymentProcessAsync(string cardNumber, string cardHolderName, string cardExpirationDate, string cardSecurityNumber, decimal amount)
        {
            // Simulate an external payment processing delay
            await Task.Delay(2000);
           
          return (true, null);
        }
    }
}
