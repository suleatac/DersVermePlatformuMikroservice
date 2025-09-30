using MediatR;
using Microservice.Payment.Api.Repositories;
using Microservice.Shared;
using Microservice.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Payment.Api.Features.Payment.GetAllPaymentsByUserId
{
    public class GetAllPaymentsByUserIdQueryHandler(AppDbContext context, IIdentityService identityService) : IRequestHandler<GetAllPaymentsByUserIdQuery, ServiceResult<List<GetAllPaymentsByUserIdResponse>>>
    {
        public async Task<ServiceResult<List<GetAllPaymentsByUserIdResponse>>> Handle(GetAllPaymentsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userId = identityService.GetUserId;
            var payments = await context.Payments
                .Where(p => p.UserId == userId)
                .Select(p => new GetAllPaymentsByUserIdResponse(p.Id, p.OrderCode, p.Amount.ToString("C"), p.Created, p.Status))
                .ToListAsync(cancellationToken);


            return ServiceResult<List<GetAllPaymentsByUserIdResponse>>.SuccessAsOK(payments);
        }

 



    }
}
