using Microservice.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Order.Application.Contracts.Repositories
{
    public interface IOrderRepository:IGenericRepository<Guid, Domain.Entities.Order>
    {
        Task<List<Domain.Entities.Order>> GetOrdersByBuyerIdAsync(Guid buyerId);
        Task SetStatus(string orderCode, Guid? paymentId, OrderStatus status);
    }
}
