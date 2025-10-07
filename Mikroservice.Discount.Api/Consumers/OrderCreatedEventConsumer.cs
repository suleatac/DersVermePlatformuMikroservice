using Microservice.Bus.Events;
using Mikroservice.Discount.Api.Features.Discounts;
using Mikroservice.Discount.Api.Repositories;
using System.Threading;

namespace Microservice.Discount.Api.Consumers
{
    public class OrderCreatedEventConsumer(IServiceProvider serviceProvider) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
          using var scope = serviceProvider.CreateScope();
           var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var discount = new Mikroservice.Discount.Api.Features.Discounts.Discount()
            {

                Id = NewId.NextSequentialGuid(),
                Code = DiscountCodeGenerator.GenerateCode(10),
                Rate = 0.1f,
                UserId = context.Message.UserId,
                ExpiredTime = DateTime.Now.AddMonths(1),
                CreatedTime = DateTime.Now
            };
            await appDbContext.Discounts.AddAsync(discount);
            await appDbContext.SaveChangesAsync();
        }
    }
}
