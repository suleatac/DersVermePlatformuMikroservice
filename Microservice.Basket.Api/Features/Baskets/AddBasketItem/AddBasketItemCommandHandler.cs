using MassTransit;
using MediatR;
using Microservice.Basket.Api.Const;
using Microservice.Basket.Api.Features.Baskets.Data;
using Microservice.Basket.Api.Features.Baskets.Dtos;
using Microservice.Shared;
using Microservice.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Threading;

namespace Microservice.Basket.Api.Features.Baskets.AddBasketItem
{

    public class AddBasketItemCommandHandler(IIdentityService identityService,BasketService basketService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
    
            var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);
            Data.Basket? currentBasket;
            var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice, null);

            //fast fail

            if (string.IsNullOrEmpty(basketAsJson))
            {
                //currentBasket = new Data.Basket(userId, [newBasketItem]);
                currentBasket = new Data.Basket(identityService.GetUserId, new List<BasketItem> { newBasketItem });
                await basketService.CreateCacheAsync(currentBasket, cancellationToken);
                return ServiceResult.SuccessAsNoContent();
            }

            currentBasket = System.Text.Json.JsonSerializer.Deserialize<Data.Basket>(basketAsJson);
            var existBasketItem = currentBasket?.Items.FirstOrDefault(x => x.Id == request.CourseId);

            if (existBasketItem is not null)
            {
                currentBasket?.Items.Remove(existBasketItem);
        
        
            }
            currentBasket?.Items.Add(newBasketItem);








            currentBasket?.ApplyAvailableDiscount();









            await basketService.CreateCacheAsync(currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();

        }

     
    }
}
