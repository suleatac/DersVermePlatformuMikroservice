using Microservice.Order.Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Order.Persistence.Repositories
{
    public class OrderRepository(AppDbContext context): GenericRepository<Guid, Domain.Entities.Order>(context), IOrderRepository
    {


        public Task<List<Domain.Entities.Order>> GetOrdersByBuyerIdAsync(Guid buyerId)
        {
            var orders = context.Orders.Where(o => o.BuyerId == buyerId).Include(x=>x.OrderItems).OrderByDescending(x=>x.Created).ToListAsync();
            return orders;
        }






    }
}
