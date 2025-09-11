using AutoMapper;
using MediatR;
using Microservice.Catalog.Api.Features.Categories.Create;

using Microservice.Catalog.Api.Repositories;
using Microservice.Shared;
using Microservice.Shared.Extentions;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Catalog.Api.Features.Categories.GetAll
{

    public class GetAllCategoryQuery: IRequestByServiceResult<List<CategoryDto>>;

    public class  GetAllCategoryQueryHandler(AppDbContext context, IMapper mapper): IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
    {
        public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await context.Categories.ToListAsync(cancellationToken:cancellationToken);
            var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);
            return ServiceResult<List<CategoryDto>>.SuccessAsOK(categoriesAsDto);
        }
    }



   
    public static class GetAllCategoryEndPoint
    {

        public static RouteGroupBuilder GetAllCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", 
                async (IMediator mediator) =>
                (await mediator.Send(new GetAllCategoryQuery())).ToGenericResult())
                  .WithName("GetAllCategoryQuery"); 
            return group;
        }

    }
}
