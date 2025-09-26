using MediatR;
using Microservice.Order.Application.Contracts.Repositories;
using Microservice.Order.Application.Contracts.UnitOfWorks;
using Microservice.Order.Domain.Entities;
using Microservice.Shared;
using Microservice.Shared.Services;
using System.Net;

namespace Microservice.Order.Application.Features.Orders.Create
{
    public class CreateOrderCommandHandler
        (
           IOrderRepository orderRepository,
           IGenericRepository<int, Address> addressRepository,
           IIdentityService identityService,
           IUnitOfWork unitOfWork
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
                    identityService.GetUserId,
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

  


            var paymentId = Guid.NewGuid();

            //pAYMENT işlemlerİ yapılacak


            order.MarkAsPaid(paymentId);

            orderRepository.Update(order);
            await unitOfWork.CommitChangesAsync(cancellationToken);



            return ServiceResult.SuccessAsNoContent();


        }
    }
}
