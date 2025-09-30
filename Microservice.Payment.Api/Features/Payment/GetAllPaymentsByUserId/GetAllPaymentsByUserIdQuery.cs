using Microservice.Shared;

namespace Microservice.Payment.Api.Features.Payment.GetAllPaymentsByUserId
{
    public record GetAllPaymentsByUserIdQuery :IRequestByServiceResult<List<GetAllPaymentsByUserIdResponse>>;
}
