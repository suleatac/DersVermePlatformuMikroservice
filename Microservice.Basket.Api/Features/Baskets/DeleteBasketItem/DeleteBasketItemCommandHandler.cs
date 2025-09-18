using MediatR;
using Microservice.Basket.Api.Const;
using Microservice.Basket.Api.Features.Baskets.AddBasketItem;
using Microservice.Basket.Api.Features.Baskets.Dtos;
using Microservice.Shared;
using Microservice.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace Microservice.Basket.Api.Features.Baskets.DeleteBasketItem
{
  
    public class DeleteBasketItemCommandHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            Guid userId = identityService.GetUserId;
            var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);
            var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);
          
 
            //fast fail

            if (string.IsNullOrEmpty(basketAsString))
            {
                
                return ServiceResult.Error("Basket not found", System.Net.HttpStatusCode.NotFound);
            }

            var currentBasket = System.Text.Json.JsonSerializer.Deserialize<BasketDto>(basketAsString);
          ;
            var BasketItemtoDelete = currentBasket?.BasketItems.FirstOrDefault(x => x.Id == request.basketId);

            if (BasketItemtoDelete is null)
            {
               return ServiceResult.Error("Basket item not found", System.Net.HttpStatusCode.NotFound);

            }
            currentBasket?.BasketItems.Remove(BasketItemtoDelete);


           basketAsString = System.Text.Json.JsonSerializer.Serialize(currentBasket);
            await distributedCache.SetStringAsync(cacheKey, basketAsString, token: cancellationToken);

            return ServiceResult.SuccessAsNoContent();



        }

        private async Task CreateCacheAsync(BasketDto basket, string cacheKey, CancellationToken cancellationToken)
        {
            var basketAsString = System.Text.Json.JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(cacheKey, basketAsString, token: cancellationToken);
        }
    }
}
