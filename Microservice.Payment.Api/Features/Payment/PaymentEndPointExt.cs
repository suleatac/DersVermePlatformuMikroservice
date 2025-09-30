using Asp.Versioning.Builder;
using Microservice.Payment.Api.Features.Payment.Create;
using Microservice.Payment.Api.Features.Payment.GetAllPaymentsByUserId;

namespace Microservice.Payment.Api.Features.Payment
{
  
    public static class PaymentEndPointExt
    {
        public static void AddPaymentGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            var group = app.MapGroup("/api/v{version:apiVersion}/payments").WithTags("Payment");
            group.CreatePaymentGroupItemEndPoint();
            group.GetAllPaymentsByUserIdGroupItemEndPoint();
            group.WithApiVersionSet(apiVersionSet);
        }


    }
}
