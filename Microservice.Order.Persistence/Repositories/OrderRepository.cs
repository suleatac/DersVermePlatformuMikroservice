using Microservice.Order.Application.Contracts.Repositories;
using Microservice.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Order.Persistence.Repositories
{
    public class OrderRepository(AppDbContext context) : GenericRepository<Guid, Domain.Entities.Order>(context), IOrderRepository
    {
      
        public async Task<List<Domain.Entities.Order>> GetOrdersByBuyerIdAsync(Guid buyerId)
        {
            // await eklendi
            var orders = await context.Orders
                .Where(o => o.BuyerId == buyerId)
                .Include(x => x.OrderItems)
                .OrderByDescending(x => x.Created)
                .ToListAsync();

            return orders;
        }


        public async Task SetStatus(string orderCode, Guid? paymentId, OrderStatus status)
        {
            var order = await context.Orders.FirstOrDefaultAsync(x => x.Code == orderCode);

            // Order bulunamazsa kontrol ekledik
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with code {orderCode} not found");
            }

            order.PaymentId = paymentId ?? order.PaymentId; // Null ise eski değeri koru
            order.Status = status;
            context.Orders.Update(order);

            // Değişiklikleri kaydetme işlemini ekledik
            await context.SaveChangesAsync();
        }
    }
}
