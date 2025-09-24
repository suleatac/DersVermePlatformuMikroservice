using MediatR;
using Microservice.Basket;
using Microservice.Basket.Api.Const;
using Microservice.Basket.Api.Features.Baskets.AddBasketItem;
using Microservice.Basket.Api.Features.Baskets.Dtos;
using Microservice.Shared;
using Microservice.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace Microservice.Basket.Api.Features.Baskets.DeleteBasketItem
{
  
    public class DeleteBasketItemCommandHandler( BasketService basketService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);


            //fast fail

            if (string.IsNullOrEmpty(basketAsJson))
            {
                
                return ServiceResult.Error("Basket not found", System.Net.HttpStatusCode.NotFound);
            }

            var currentBasket = System.Text.Json.JsonSerializer.Deserialize<Data.Basket>(basketAsJson);
          
            var BasketItemtoDelete = currentBasket?.Items.FirstOrDefault(x => x.Id == request.basketId);

            if (BasketItemtoDelete is null)
            {
               return ServiceResult.Error("Basket item not found", System.Net.HttpStatusCode.NotFound);

            }
            currentBasket?.Items.Remove(BasketItemtoDelete);


           basketAsJson = System.Text.Json.JsonSerializer.Serialize(currentBasket);
            await basketService.CreateCacheAsync(currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();



        }

       
    }
}
