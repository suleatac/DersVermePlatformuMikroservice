using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Order.Application.Contracts.Refit.PaymentService
{
    public interface IPaymentService
    {
        [Post("/api/v1/payments")]
        Task<CreatePaymentResponse> CreateAsync([Body] CreatePaymentRequest request);

        [Post("/api/v1/payments/status/{orderCode}")]
        Task<GetPaymentStatusResponse> GetStatusAsync([Body] string orderCode);
    }
}
