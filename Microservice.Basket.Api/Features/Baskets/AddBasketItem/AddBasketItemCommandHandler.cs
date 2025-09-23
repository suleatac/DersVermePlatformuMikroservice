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

    public class AddBasketItemCommandHandler(IDistributedCache distributedCache,IIdentityService identityService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            Guid userId = identityService.GetUserId;
            var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);
            var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);
            Data.Basket? currentBasket;
            var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice, null);

            //fast fail

            if (string.IsNullOrEmpty(basketAsString))
            {
                //currentBasket = new Data.Basket(userId, [newBasketItem]);
                currentBasket = new Data.Basket(userId, new List<BasketItem> { newBasketItem });
                await CreateCacheAsync(currentBasket!, cacheKey, cancellationToken);
                return ServiceResult.SuccessAsNoContent();
            }

            currentBasket = System.Text.Json.JsonSerializer.Deserialize<Data.Basket>(basketAsString);
            var existBasketItem = currentBasket?.Items.FirstOrDefault(x => x.Id == request.CourseId);

            if (existBasketItem is not null)
            {
                currentBasket?.Items.Remove(existBasketItem);
        
        
            }
            currentBasket?.Items.Add(newBasketItem);


            await CreateCacheAsync(currentBasket!, cacheKey, cancellationToken);

            return ServiceResult.SuccessAsNoContent();

        }

        private async Task CreateCacheAsync( Data.Basket basket, string cacheKey, CancellationToken cancellationToken)
        {
           var basketAsString = System.Text.Json.JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(cacheKey, basketAsString, token: cancellationToken);
        }
    }
}
