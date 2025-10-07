using MediatR;
using Microservice.Payment.Api.Repositories;
using Microservice.Shared;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Payment.Api.Features.Payment.GetStatus
{
    public record GetPaymentStatusRequest(string orderCode):IRequestByServiceResult<GetPaymentStatusResponse>;
    public record GetPaymentStatusResponse(Guid? PaymentId,bool IsPaid);
    public class GetPaymentStatusQueryHandler(AppDbContext appDbContext) : IRequestHandler<GetPaymentStatusRequest, ServiceResult<GetPaymentStatusResponse>>
    {
        public async Task<ServiceResult<GetPaymentStatusResponse>> Handle(GetPaymentStatusRequest request, CancellationToken cancellationToken)
        {
       
            var payment = await appDbContext.Payments.FirstOrDefaultAsync(p => p.OrderCode == request.orderCode,cancellationToken:cancellationToken);

            if (payment == null)
            {
                return ServiceResult<GetPaymentStatusResponse>.SuccessAsOK(new GetPaymentStatusResponse(payment.Id, false));
            }

            return ServiceResult<GetPaymentStatusResponse>.SuccessAsOK(new GetPaymentStatusResponse(payment.Id,payment.Status==PaymentStatus.Success));


        }
    }
}
