using MassTransit;
using MediatR;
using Microservice.Order.Application.Contracts.Refit.PaymentService;
using Microservice.Order.Application.Contracts.Repositories;
using Microservice.Order.Application.Contracts.UnitOfWorks;
using Microservice.Order.Domain.Entities;
using Microservice.Shared;
using Microservice.Shared.Services;
using System.Net;

namespace Microservice.Order.Application.Features.Orders.CreateOrder
{
    public class CreateOrderCommandHandler
        (
           IOrderRepository orderRepository,
           IGenericRepository<int, Address> addressRepository,
           IIdentityService identityService,
           IUnitOfWork unitOfWork,
           IPublishEndpoint publishEndpoint,
           IPaymentService paymentService
        )
        : IRequestHandler<CreateOrderCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            if (!request.Items.Any())
                return ServiceResult.Error("No items in the order", "Order must have at least one item", HttpStatusCode.BadRequest);





  


            //TODO:begin transaction


            var newAddress = new Address
            {
                Province = request.Address.Province,
                District = request.Address.District,
                Street = request.Address.Street,
                ZipCode = request.Address.ZipCode,
                Line = request.Address.Line
            };

        
            var order = Domain.Entities.Order.CreateUnPaidOrder
                (
                    identityService.UserId,
                    newAddress.Id,
                    request.DiscountRate
                );

            foreach (var orderItem in request.Items)
            {
                order.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice);
            }

            order.Address = newAddress;
            orderRepository.Add(order);

            await unitOfWork.CommitChangesAsync(cancellationToken);

            CreatePaymentRequest paymentRequest = new CreatePaymentRequest(order.Code,request.Payment.CardNumber, request.Payment.CardName, request.Payment.Expiration, request.Payment.CVV,order.TotalPrice);
           var paymentResponse = await paymentService.CreateAsync(paymentRequest);

            if(paymentResponse.Status==false || paymentResponse.PaymentId==null)
            {
                return ServiceResult.Error("Payment failed", paymentResponse.ErrorMessage ?? "Payment service returned an error", HttpStatusCode.InternalServerError);
            }


            order.MarkAsPaid(paymentResponse.PaymentId!.Value);

            orderRepository.Update(order);
            await unitOfWork.CommitChangesAsync(cancellationToken);

            await publishEndpoint.Publish(new Bus.Events.OrderCreatedEvent(order.Id,identityService.UserId),cancellationToken);

            return ServiceResult.SuccessAsNoContent();


        }
    }
}
