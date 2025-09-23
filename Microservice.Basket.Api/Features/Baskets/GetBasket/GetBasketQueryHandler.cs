using AutoMapper;
using MediatR;
using Microservice.Basket.Api.Const;
using Microservice.Basket.Api.Features.Baskets.Dtos;
using Microservice.Shared;
using Microservice.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Microservice.Basket.Api.Features.Baskets.GetBasket
{
    public class GetBasketQueryHandler(IDistributedCache distributedCache,IIdentityService identityService, IMapper mapper) : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {


            Guid userId = identityService.GetUserId;
            var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);
            var basketAsString = await distributedCache.GetStringAsync(cacheKey, token: cancellationToken);
            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult<BasketDto>.Error("Basket not found", System.Net.HttpStatusCode.NotFound);
            }
            var currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

            var basketDto = mapper.Map<BasketDto>(currentBasket);


            return ServiceResult<BasketDto>.SuccessAsOK(basketDto!);
        }
    }
}
