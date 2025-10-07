using Microservice.Basket.Api.Const;
using Microservice.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading;

namespace Microservice.Basket.Api.Features.Baskets
{
    public class BasketService(IIdentityService identityService,IDistributedCache distributedCache)
    {
        private string GetCacheKey(Guid userId)=> string.Format(BasketConst.BasketCacheKey, userId);
        private string GetCacheKey()
        {
            return string.Format(BasketConst.BasketCacheKey, identityService.UserId);
        }

        public Task<string?> GetBasketFromCache(CancellationToken cancellationToken)
        {
           
           return  distributedCache.GetStringAsync(GetCacheKey(), token:cancellationToken);
        }

        public async Task CreateCacheAsync(Data.Basket basket,CancellationToken cancellationToken)
        {
            var basketAsString = System.Text.Json.JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(GetCacheKey(), basketAsString, token: cancellationToken);
        }
        public async Task DeleteBasket(Guid userId)
        {
         await distributedCache.RemoveAsync(GetCacheKey(userId));
        }

    }
}
