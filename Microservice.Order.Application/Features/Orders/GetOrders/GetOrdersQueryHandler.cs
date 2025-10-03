using AutoMapper;
using MediatR;
using Microservice.Order.Application.Contracts.Repositories;
using Microservice.Order.Application.Features.Orders.CreateOrder;
using Microservice.Shared;
using Microservice.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Order.Application.Features.Orders.GetOrders
{

    public class GetOrdersQueryHandler(IIdentityService identityService, IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<GetOrdersQuery, ServiceResult<List<GetOrdersResponse>>>
    {
        public async Task<ServiceResult<List<GetOrdersResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetOrdersByBuyerIdAsync(identityService.UserId);


            if (!orders.Any())
                return ServiceResult<List<GetOrdersResponse>>.Error("No orders found for the user", "The user has no orders", HttpStatusCode.NotFound);

            var response = orders.Select(o => new GetOrdersResponse(o.Created, o.TotalPrice, mapper.Map<List<OrderItemDto>>(o.OrderItems))).ToList();
            return ServiceResult<List<GetOrdersResponse>>.SuccessAsOK(response);
        }
     
    }
}
