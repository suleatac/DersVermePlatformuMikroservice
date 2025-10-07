using Microservice.Order.Application.Contracts.Refit.PaymentService;
using Microservice.Order.Application.Contracts.Repositories;
using Microservice.Order.Application.Contracts.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Order.Application.BackgroundServices
{
    public class CheckPaymentStatusOrderBackgroundService(IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceProvider.CreateScope();
            var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
            var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var orders = orderRepository.Where(x => x.Status == Domain.Entities.OrderStatus.waitingForPayment).ToList();

            foreach (var order in orders)
            {
                var paymentStatusResponse = await paymentService.GetStatusAsync(order.Code);
                if (paymentStatusResponse.isPaid)
                {

                    await orderRepository.SetStatus(order.Code,paymentStatusResponse.PaymentId!, Domain.Entities.OrderStatus.Paid);
                    await unitOfWork.CommitChangesAsync(stoppingToken);
                }
            }


        }
    }
}
